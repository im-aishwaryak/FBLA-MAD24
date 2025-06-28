using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Firebase.Auth;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
