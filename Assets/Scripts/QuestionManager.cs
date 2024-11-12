using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Text questionText;
    public Text scoreText;
    public Text finalScore;
    public Button[] replyButtons;
    public QtsData qtsData; // Reference to the scriptable object
    public GameObject right;
    public GameObject wrong;
    public GameObject gameFinished;

    private int currentQuestion = 0;
    private static int score = 0;

    void Start()
    {
        SetQuestion(currentQuestion);
        right.SetActive(false);
        wrong.SetActive(false);
        gameFinished.SetActive(false);
    }

    void SetQuestion(int questionIndex)
    {
        questionText.text = qtsData.questions[questionIndex].questionText;
        // Remove previous listeners before adding new ones
        foreach (Button r in replyButtons)
        {
            r.onClick.RemoveAllListeners();
        }
        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].GetComponentInChildren<Text>().text = qtsData.questions[questionIndex].replies[i];
            int replyIndex = i;
            replyButtons[i].onClick.AddListener(() => CheckReply(replyIndex));
        }
    }

    void CheckReply(int replyIndex)
    {
        if (replyIndex == qtsData.questions[currentQuestion].correctReplyIndex)
        {
            score++;
            scoreText.text = score.ToString();
            // Enable right reply panel
            right.SetActive(true);
            // Disable all reply buttons
            foreach (Button r in replyButtons)
            {
                r.interactable = false;
            }
            // Next Question
            StartCoroutine(Next());
        }
        else
        {
            // Wrong reply
            wrong.SetActive(true);
            // Disable all reply buttons
            foreach (Button r in replyButtons)
            {
                r.interactable = false;
            }
            // Next Question
            StartCoroutine(Next());
        }
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(2);
        right.SetActive(false);
        wrong.SetActive(false);
        currentQuestion++;
        if (currentQuestion < qtsData.questions.Length)
        {
            SetQuestion(currentQuestion);
        }
        else
        {
            gameFinished.SetActive(true);
            // Calculate the score percentage
            float scorePercentage = (float)score / qtsData.questions.Length * 100;
            // Display the score percentage
            finalScore.text = "You scored " + scorePercentage.ToString("F0") + "%";
            // Display the appropriate message based on the score percentage
            if (scorePercentage < 50)
            {
                finalScore.text += "\nGame Over";
            }
            else if (scorePercentage < 60)
            {
                finalScore.text += "\nKeep Trying";
            }
            else if (scorePercentage < 70)
            {
                finalScore.text += "\nGood Job";
            }
            else if (scorePercentage < 80)
            {
                finalScore.text += "\nWell Done!";
            }
            else
            {
                finalScore.text += "\nYou're a genius!";
            }
        }
    }

    public void Reset()
    {
        // Hide both the "Well done" and "Wrong" panels
        right.SetActive(false);
        wrong.SetActive(false);
        // Enable all reply buttons
        foreach (Button r in replyButtons)
        {
            r.interactable = true;
        }
        // Set the next question
        SetQuestion(currentQuestion);
    }
}
