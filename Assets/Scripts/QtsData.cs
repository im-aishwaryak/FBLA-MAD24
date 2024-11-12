using UnityEngine;

[CreateAssetMenu(fileName = "New QuestionData", menuName = "QuestionData")]
public class QtsData : ScriptableObject
{
    [System.Serializable]
    public struct Question
    {
        public string questionText; // stores qtestion 
        public string[] replies; // stores explanation
        public int correctReplyIndex; // stores correct answer's index
    }

    public Question[] questions; // holds a collection of questions and their associated data
}