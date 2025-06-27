using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Firebase.Auth;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            Debug.Log("YAYYY"); 

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
        //explanationText.text = $"Previous Answer: {explanation}";

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
            var questionsList = selectedTrail.checkpoints[checkpoint].questions;
            currentQuestions = questionsList.ToArray();

            DisplayQuestion();
        }
        else
        {
            Debug.LogError("Trail or trailDict is null or has no checkpoints.");
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
                    Debug.Log("OMG");
                    trailDict = ConvertTrailToDict(newTrail);
                    currentTrails.Add(trailDict);
                    // Update the trails field in Firebase
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
        {"questions", new List<Dictionary<string, object>>()}
    }; 

        return trailDict;
    }

    public Dictionary<string, object> getTrail()
    {
        return trailDict; 
    }

}
