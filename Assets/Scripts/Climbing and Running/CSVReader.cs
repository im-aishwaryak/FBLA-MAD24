using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers = new string[4]; // 4 answers (A, B, C, D)
    public int correctAnswerIndex;
    public string explanation;
}

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile; // Assign the CSV file in the Inspector
    public List<Question> questions = new List<Question>();

    // Start is called before the first frame update
    void Awake()
    {
        LoadQuestionsFromCSV();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadQuestionsFromCSV()
    {
        // Split CSV by new line, remove any empty entries
        string[] lines = csvFile.text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        Debug.Log("Lines Count: " + lines.Length);  // Log the number of lines in the CSV file

        // Ensure there is at least one line in the CSV file (skip header)
        if (lines.Length <= 1)
        {
            Debug.LogError("CSV file does not contain any questions after the header.");
            return;
        }

        for (int i = 1; i < lines.Length; i++) // Start from 1 to skip header if there is one
        {
            string[] values = lines[i].Split(',');

            // Skip incomplete rows
            if (values.Length < 9)  // We need at least 9 columns
            {
                continue;
            }

            Question question = new Question
            {
                questionText = values[2].Trim(), // Question is at index 2
                answers = new string[] {
                    values[3].Trim(), // Answer A (index 3)
                    values[4].Trim(), // Answer B (index 4)
                    values[5].Trim(), // Answer C (index 5)
                    values[6].Trim()  // Answer D (index 6)
                },
                explanation = values[8].Trim()  // Explanation is at index 8
            };

            // Handle the correct answer index
            string correctAnswerString = values[7].Trim(); // Correct answer is at index 7
            if (!string.IsNullOrEmpty(correctAnswerString))
            {
                // Try to parse the correct answer index, log an error if it fails
                if (int.TryParse(correctAnswerString, out int correctIndex))
                {
                    question.correctAnswerIndex = correctIndex - 1; // Adjust for zero-based indexing
                }
                else
                {
                    Debug.LogError("Invalid correct answer index for question: " + question.questionText);
                    continue; // Skip this question if the correct answer index is invalid
                }
            }
            else
            {
                Debug.LogError("Missing correct answer index for question: " + question.questionText);
                continue;
            }

            // Add the question to the list
            questions.Add(question);
        }

        // Check if no questions were loaded and log an error
        if (questions.Count == 0)
        {
            Debug.LogError("No questions were loaded from the CSV.");
        }
    }
}
