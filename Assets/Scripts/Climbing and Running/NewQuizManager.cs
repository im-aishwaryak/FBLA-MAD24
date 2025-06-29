using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Firebase.Auth;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static JsonReader;

public class NewQuizManager : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text scoreText;
    public Text explanationText;

    [SerializeField] private JsonReader jsonReader;  // Reference to your new JSON reader script

    private int currentQuestionIndex = 0;
    private double score = 0;
    private double questionCount = 0;

    public string subject;

    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color defaultColor = Color.white;

    public SpriteChanger spriteChanger;
    public WallChanger wallChanger;
    public JsonReader.Trail selectedTrail;
    public Dictionary<string, object> trailDict;
    public FirebaseManager dataHandler; 

    // Store the current list of questions
    private JsonReader.Question[] currentQuestions;

    void Start()
    {
        StopAllCoroutines();
        subject = SubjectManager.selectedSubject; 

        jsonReader.LoadJSON("question_bank");  // Make sure the filename matches
        Debug.Log(subject);
    

        if (jsonReader.trailData != null && jsonReader.trailData.trails.Count > 0)
        {
            selectedTrail = jsonReader.trailData.trails.Find(t => t.trailID == subject);
            StartCoroutine(SaveTrailCoroutine(selectedTrail));
        }
        else
        {
            Debug.LogError("Trail data missing or not loaded.");
        }
    }

    void OnEnable()
    {
        StopAllCoroutines(); // Clean slate whenever this GameObject becomes active
    }


    void DisplayQuestion()
    {
        Debug.Log("Current question index: " + currentQuestionIndex + "  questions length = " + currentQuestions.Length); 
        questionCount++;

        if (currentQuestionIndex >= currentQuestions.Length)
        {
            currentQuestionIndex = 0;  // Loop back
        }

        JsonReader.Question currentQuestion = currentQuestions[currentQuestionIndex];

        questionText.text = currentQuestion.question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];

            int index = i;
            answerButtons[i].onClick.RemoveAllListeners(); // Prevent stacking
            answerButtons[i].onClick.AddListener(() =>
            StartCoroutine(OnAnswerClicked(index, currentQuestion.correctAnswer, currentQuestion.correctExplanation)));
        }
    }

    private IEnumerator OnAnswerClicked(int selectedAnswerIndex, int correctAnswerIndex, string explanation)
    {
        if (selectedAnswerIndex == correctAnswerIndex)
        {
            score++;
            wallChanger.Move(true);
            spriteChanger.climb(3);
            ChangeButtonColorImmediately(answerButtons[selectedAnswerIndex], correctColor);
        }
        else
        {
            wallChanger.Move(false);
            ChangeButtonColorImmediately(answerButtons[selectedAnswerIndex], wrongColor);
        }

        double accuracy = Math.Round((score / questionCount) * 100);
        scoreText.text = accuracy + "%";
        string correctAnswerText = currentQuestions[currentQuestionIndex].answers[correctAnswerIndex];
        string userAnswerText = currentQuestions[currentQuestionIndex].answers[selectedAnswerIndex]; 
        explanationText.text = $"Previous Answer: {explanation}";

        //var saveTask3 = SaveQuestion(questionText.text, correctAnswerText, userAnswerText, explanationText.text);
        //yield return new WaitUntil(() => saveTask3.IsCompleted);

        yield return new WaitForSeconds(0.5f);

        Debug.Log("yo!"); 
        foreach (Button button in answerButtons)
        {
            button.onClick.RemoveAllListeners();
            ChangeButtonColorImmediately(button, defaultColor);
        }

        currentQuestionIndex++;
        DisplayQuestion();
      
    }

    /*public async Task SaveQuestion(string questionText, string correctAnswerText, string userAnswerText, string explanationText)
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user == null)
        {
            Debug.LogError("No user logged in");
            return;
        }

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(user.Email);

        try
        {
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                List<object> currentTrails = snapshot.GetValue<List<object>>("trails");
                int trailIndex = getTrailIndex(currentTrails);
                int checkpointIndex = Convert.ToInt32(trailDict["currentCheckpoint"]); 
            }
        }
        catch(Exception e)
        {
            Debug.LogError($"Error saving checkpoint: {e.Message}");
        }
    }*/

    private void ChangeButtonColorImmediately(Button button, Color targetColor)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = targetColor;
        }
    }

    private IEnumerator SaveTrailCoroutine(JsonReader.Trail trail)
    {
        var saveTask = SaveTrail(trail);
        yield return new WaitUntil(() => saveTask.IsCompleted);

        //Ensure trailDict is assigned now
        if (selectedTrail != null && selectedTrail.checkpoints.Count > 0 && trailDict != null)
        {
            int checkpoint = Convert.ToInt32(trailDict["currentCheckpoint"]);
            Debug.Log("current checkpoint: " + checkpoint);

            var saveTask2 = SaveCheckpoint(checkpoint);
            yield return new WaitUntil(() => saveTask2.IsCompleted); 

            var questionsList = selectedTrail.checkpoints[checkpoint].questions;
            currentQuestions = questionsList.ToArray();

            DisplayQuestion();
        }
        else
        {
            Debug.LogError("Trail or trailDict is null or has no checkpoints.");
        }
    }

    public async Task SaveCheckpoint(int checkpoint)
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user == null)
        {
            Debug.LogError("No user logged in");
            return;
        }

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(user.Email);

        try
        {
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                List<object> currentTrails = snapshot.GetValue<List<object>>("trails");
                int trailIndex = getTrailIndex(currentTrails);

                if (trailIndex >= 0 && currentTrails[trailIndex] is Dictionary<string, object> trail)
                {
                    Dictionary<string, object> checkpoints;

                    if (trail.ContainsKey("checkpoints") && trail["checkpoints"] is Dictionary<string, object> existingCheckpoints)
                    {
                        checkpoints = existingCheckpoints;
                    }
                    else
                    {
                        checkpoints = new Dictionary<string, object>();
                    }

                    // Only add if not already added
                    if (!checkpoints.ContainsKey(checkpoint.ToString()))
                    {
                        var selectedCP = selectedTrail.checkpoints[checkpoint];
                        var checkpointDict = convertCheckpointToDict(selectedCP);
                        checkpoints[checkpoint.ToString()] = checkpointDict;

                        // Put the updated checkpoints dictionary back into the trail
                        trail["checkpoints"] = checkpoints;

                        // Replace the trail in the list
                        currentTrails[trailIndex] = trail;

                        // 🔥 Now update the full trails list
                        await docRef.UpdateAsync("trails", currentTrails);
                    }
                }
                else
                {
                    Debug.LogError("Trail not found at index: " + trailIndex);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving checkpoint: {e.Message}");
        }
    }


    public async Task SaveTrail(JsonReader.Trail newTrail)
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user == null)
        {
            Debug.LogError("No user logged in");
            return;
        }

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(user.Email);

        try
        {
            // Get the current user document
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                // Get current trails list
                List<object> currentTrails = new List<object>();

                if (snapshot.ContainsField("trails"))
                {
                    currentTrails = snapshot.GetValue<List<object>>("trails");
                }

                // Check if trail already exists (by trailID)
                bool trailExists = false;
                foreach (var trailObj in currentTrails)
                {
                    if (trailObj is Dictionary<string, object> trailDictionary)
                    {
                        if (trailDictionary.ContainsKey("trailID") &&
                            trailDictionary["trailID"].ToString() == newTrail.trailID)
                        {
                            trailExists = true;
                            trailDict = trailDictionary; 
                            Debug.Log($"Trail {newTrail.trailID} already exists in Firebase");
                            break;
                        }
                    }
                }

                // If trail doesn't exist, add it
                if (!trailExists)
                {
                    trailDict = ConvertTrailToDict(newTrail);
                    currentTrails.Add(trailDict);
                    
                    await docRef.UpdateAsync("trails", currentTrails);
                    //Debug.Log($"Trail {newTrail.trailID} added to Firebase");
                }

                TrailManager.setTrail(trailDict);
            }
            else
            {
                Debug.LogError("User document doesn't exist");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving trail: {e.Message}");
        }
    }


    public Dictionary<string, object> ConvertTrailToDict(JsonReader.Trail trail)
    {
        var trailDict = new Dictionary<string, object>
    {
        { "trailID", trail.trailID },
        { "title", trail.title },
        { "desc", trail.desc },
        { "subject", trail.subject },
        {"status", "in progress"},
        {"currentCheckpoint", 0 },
        {"checkpoints", new Dictionary<string, object>()}
    }; 
        return trailDict;
    }

    public Dictionary<string, object> convertCheckpointToDict(JsonReader.Checkpoint checkpoint)
    {
        var checkpointDict = new Dictionary<string, object>
        {
            { "checkpointID", checkpoint.title },
            {"desc", checkpoint.desc },
            {"questions", new Dictionary<string, object>()},
            {"accuracy", null}
        };
        return checkpointDict; 
    }

    public int getTrailIndex(List<object> currentTrails)
    {
        int index = 0; 
        foreach (var trailObj in currentTrails)
        {
            if (trailObj is Dictionary<string, object> trailDictionary)
            {
                if (trailDictionary.ContainsKey("trailID") &&
                    trailDictionary["trailID"].ToString().Equals(trailDict["trailID"]))
                {
                    return index;
                }
            }
            index++; 
        }
        return 0; 
    }

    public Dictionary<string, object> getTrail(List<object> currentTrails)
    {
        foreach (var trailObj in currentTrails)
        {
            if (trailObj is Dictionary<string, object> trailDictionary)
            {
                if (trailDictionary.ContainsKey("trailID") &&
                    trailDictionary["trailID"].ToString().Equals(trailDict["trailID"]))
                {
                    return trailDictionary;
                }
            }
        }
        return null;
    }
}

