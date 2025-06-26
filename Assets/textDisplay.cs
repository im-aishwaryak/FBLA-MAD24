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
    public int checkpoints;
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
                findTrail(subjectName);
            }

            

            // Find the Text component in the button's children
            //Text btnText = btn.GetComponentInChildren<Details>();
            //Text btnText = btn.transform.Find("Details").GetComponent<Text>();

            // OR for TMPro
            TextMeshProUGUI btnText = btn.transform.Find("Details").GetComponent<TextMeshProUGUI>();

            if (btnText != null)
            {
                // Set the text based on button name (customize this logic as needed)
                btnText.text = "Explored " + checkpoints + " of 4 checkpoints\nNext Stop: ";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void findTrail(string subjectName)
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user == null)
        {
            Debug.LogError("No user logged in");
            return;
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
                        checkpoints = Convert.ToInt32(trail["currentCheckpoint"]); 
                        break;
                    }
                }
            }

            // Write updated list back
            await docRef.UpdateAsync("trails", trailsList);
        }
    }
}
