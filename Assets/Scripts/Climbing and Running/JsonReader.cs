using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] answers;
        public int correctAnswer;
        public string correctExplanation;
    }

    [System.Serializable]
    public class Checkpoint
    {
        public string title;
        public string desc;
        public List<Question> questions;
    }
    
    [System.Serializable]
    public class Trail
    {
        public string trailID;
        public string title;
        public string desc;
        public string subject;
        public string grade;
        public List<Checkpoint> checkpoints;
    }

    [System.Serializable]
    public class TrailList
    {
        public List<Trail> trails;
    }

    public TrailList trailData;

    public void LoadJSON(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found: " + fileName);
            return;
        }

        trailData = JsonUtility.FromJson<TrailList>(jsonFile.text);

        if (trailData == null || trailData.trails == null || trailData.trails.Count == 0)
        {
            Debug.LogError("No data found in JSON file.");
            return;
        }

        Debug.Log("Loaded " + trailData.trails.Count + " trails from JSON.");
    }
}
