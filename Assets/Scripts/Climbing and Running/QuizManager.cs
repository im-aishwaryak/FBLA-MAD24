using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text scoreText;
    public Text explanationText;  // New Text field for the explanation

    public String subject = SubjectManager.selectedSubject; 

    [SerializeField] private CSVReader csvReader; // Reference to CSVReader
    private int currentQuestionIndex = 0 + (15 * LogicScript.level);
    private double score = 0;

    // Answer button colors
    public Color correctColor = Color.green; // Green for correct answers
    public Color wrongColor = Color.red; // Red for wrong answers
    public Color defaultColor = Color.white; // Default color

    // Controlling other GameObjects
    public SpriteChanger spriteChanger;
    public WallChanger wallChanger;
    public double questionCount = 0; 


    void Start()
    {

        Debug.Log(csvReader);
        csvReader.LoadQuestionsFromCSV(subject);

        if (csvReader.questions.Count > 0)
        {
            DisplayQuestion();
        }
        else
        {
            Debug.LogError("No questions available.");
        }
        //dialogueManager.ShowDialogue("Answer questions correctly to climb up the mountain! The more you get right, the more time you will have to collect ingredients on top!");
    }


    void DisplayQuestion()
    {
        questionCount++; 
        if (csvReader.questions.Count == 0)
        {
            Debug.LogError("No questions available.");
            return;
        }

        // Get the current question from CSV
        Question currentQuestion = csvReader.questions[currentQuestionIndex];

        // Set the question text
        questionText.text = currentQuestion.questionText;

        // Set the answer button texts
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
            int index = i; // Local copy of the index
            answerButtons[i].onClick.AddListener(() =>
            StartCoroutine(OnAnswerClicked(index, currentQuestion.correctAnswerIndex, currentQuestion.explanation)));

        }
    }

    private IEnumerator OnAnswerClicked(int selectedAnswerIndex, int correctAnswerIndex, string explanation)
    {
        // Check if the selected answer is correct
        if (selectedAnswerIndex == correctAnswerIndex)
        {
            score++; // Increase score if correct
            wallChanger.Move(selectedAnswerIndex == correctAnswerIndex);
            spriteChanger.climb(3);
            ChangeButtonColorImmediately(answerButtons[selectedAnswerIndex], correctColor);
        }
        else
        {
            wallChanger.Move(selectedAnswerIndex == correctAnswerIndex);
            ChangeButtonColorImmediately(answerButtons[selectedAnswerIndex], wrongColor);
        }

        // Update the score UI
        Debug.Log(score + "/" + questionCount); 
        double accuracy = Math.Round((score / questionCount) * 100); 
        scoreText.text = "Accuracy Rate - " + accuracy + "%";

        // Display the explanation
        explanationText.text = explanation;

        yield return new WaitForSeconds(0.5f);

        foreach (Button button in answerButtons)
        {
            button.onClick.RemoveAllListeners();
            ChangeButtonColorImmediately(button, defaultColor);
        }

        // Move to the next question
        currentQuestionIndex++;

        // Loop questions without end
        if (currentQuestionIndex >= csvReader.questions.Count)
        {
            currentQuestionIndex = 0; // Reset to the first question
        }
     

        DisplayQuestion(); // Display the next question
    }


    
    private IEnumerator WaitForNextAction(float time)
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(time);

        // Do something after waiting
        Debug.Log("Waited 0.5 seconds!");
    }
    
    // Coroutine to change the button color and then reset after 1 second
    // Directly change the button color without waiting
    private void ChangeButtonColorImmediately(Button button, Color targetColor)
    {
        Image buttonImage = button.GetComponent<Image>(); // Get the Image component of the button
        buttonImage.color = targetColor; // Set the button color to the target color
    }
    
}
