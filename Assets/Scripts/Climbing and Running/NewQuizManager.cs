using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NewQuizManager : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text scoreText;
    public Text explanationText;

    [SerializeField] private JsonReader jsonReader;  // Reference to your new JSON reader script

    private int currentQuestionIndex = 0 + (15 * LogicScript.level);
    private double score = 0;
    private double questionCount = 0;

    public string subject;

    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color defaultColor = Color.white;

    public SpriteChanger spriteChanger;
    public WallChanger wallChanger;

    // Store the current list of questions
    private JsonReader.Question[] currentQuestions;

    void Start()
    {

        jsonReader.LoadJSON("question_bank");  // Make sure the filename matches

        if (jsonReader.trailData != null && jsonReader.trailData.trails.Count > 0)
        {
            // Load the first trail with matching subject
            var selectedTrail = jsonReader.trailData.trails.Find(t => t.subject == subject);
            if (selectedTrail != null && selectedTrail.checkpoints.Count > 0)
            {
                var questionsList = selectedTrail.checkpoints[0].questions; // Use first checkpoint for now
                currentQuestions = questionsList.ToArray();

                DisplayQuestion();
            }
            else
            {
                Debug.LogError("No matching trail or checkpoints found.");
            }
        }
        else
        {
            Debug.LogError("Trail data missing or not loaded.");
        }
    }

    void DisplayQuestion()
    {
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
        scoreText.text = "Accuracy Rate - " + accuracy + "%";
        explanationText.text = explanation;

        yield return new WaitForSeconds(0.5f);

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
}
