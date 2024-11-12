using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text scoreText;
    public Text explanationText;  // New Text field for the explanation

    [SerializeField] private CSVReader csvReader; // Reference to CSVReader
    private int currentQuestionIndex = 0;
    private int score = 0;

    // Answer button colors
    public Color correctColor = Color.green; // Green for correct answers
    public Color wrongColor = Color.red; // Red for wrong answers
    public Color defaultColor = Color.white; // Default color

    // Controlling other GameObjects
    public SpriteChanger spriteChanger;
    public WallChanger wallChanger;


    void Start()
    {
        Debug.Log(csvReader);
        csvReader.LoadQuestionsFromCSV();

        if (csvReader.questions.Count > 0)
        {
            DisplayQuestion();
        }
        else
        {
            Debug.LogError("No questions available.");
        }
    }

    void DisplayQuestion()
    {
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
            answerButtons[i].onClick.AddListener(() => OnAnswerClicked(index, currentQuestion.correctAnswerIndex, currentQuestion.explanation));
        }
    }

    void OnAnswerClicked(int selectedAnswerIndex, int correctAnswerIndex, string explanation)
    {
        // Check if the selected answer is correct
        if (selectedAnswerIndex == correctAnswerIndex)
        {
            // Directly change the button color without waiting
            //ChangeButtonColorImmediately(answerButtons[selectedAnswerIndex], correctColor);
            score++; // Increase score if correct
            wallChanger.Move(selectedAnswerIndex == correctAnswerIndex);
            spriteChanger.climb(3);
        }
        else
        {
            wallChanger.Move(selectedAnswerIndex == correctAnswerIndex);
            // Directly change the button color without waiting
            //ChangeButtonColorImmediately(answerButtons[selectedAnswerIndex], wrongColor);
        }

        // Update the score UI
        scoreText.text = "Correctly Answered - " + score;

        // Display the explanation
        explanationText.text = explanation;  // Display the explanation

        // Move to the next question immediately, without delay
        currentQuestionIndex++;
        if (currentQuestionIndex < csvReader.questions.Count)
        {
            // Reset button listeners for the next question
            foreach (Button button in answerButtons)
            {
                button.onClick.RemoveAllListeners();
            }

            DisplayQuestion(); // Display the next question
        }
        else
        {
            // Optionally, display a message when the quiz is over
            questionText.text = "Quiz Over!";
            explanationText.text = ""; // Clear explanation at the end of the quiz
        }
    }

    /*
    private IEnumerator WaitForNextAction()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Do something after waiting
        Debug.Log("Waited 0.5 seconds!");
    }
    
    // Coroutine to change the button color and then reset after 1 second
    // Directly change the button color without waiting
    private void ChangeButtonColorImmediately(Button button, Color targetColor)
    {
        //Image buttonImage = button.GetComponent<Image>(); // Get the Image component of the button
        //buttonImage.color = targetColor; // Set the button color to the target color
    }
    */
}
