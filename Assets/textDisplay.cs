using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Firebase.Auth;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;

public class TextDisplay : MonoBehaviour
{

    public Button[] buttons;
    //public int checkpoints;
    public Dictionary<string, object> trail;
  
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button btn in buttons)
        {
            // Get the button's name or any property you want to use
            SubjectButtonHandler handler = btn.GetComponent<SubjectButtonHandler>();
            if (handler != null)
            {
                string subjectName = handler.subjectName;
                StartCoroutine(updateButtonText(btn, subjectName));
            }
        }
    }

 
    private IEnumerator updateButtonText(Button btn, string subjectName)
    {
        Task<int> task = findTrail(subjectName);
        yield return new WaitUntil(() => task.IsCompleted);

        int cp = task.Result;

        Text btnText = btn.transform.Find("Details").GetComponent<Text>(); // or TMP if you're using TMP
        if (btnText != null)
        {
            btnText.text = $"Explored {cp} of 4 checkpoints\nNext Stop: ";
        }

    } 

    public async Task<int> findTrail(string subjectName)
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user == null)
        {
            Debug.LogError("No user logged in");
            return 0; 
        }


        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(user.Email);

        // Step 1: Get the current user document
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists && snapshot.ContainsField("trails"))
        {
            Debug.Log("containsTrails");
            var trailsList = snapshot.GetValue<List<object>>("trails");

            for (int i = 0; i < trailsList.Count; i++)
            {
                if (trailsList[i] is Dictionary<string, object> trail)
                {
                    Debug.Log("it is a dictionary...");
                    
                    if (trail["trailID"].ToString().Equals(subjectName))
                    {
                        Debug.Log("found a match!");
                        return Convert.ToInt32(trail["currentCheckpoint"]); 
                       
                    }
                }
            }
            return 0; 
        }
        return 0;
    }
}
