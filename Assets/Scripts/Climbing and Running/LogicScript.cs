using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using Firebase.Auth;
using System;

public class LogicScript : MonoBehaviour
{
    public int score = 0;
    private bool isAlive = true;
    public GameObject gameOverScreen;
    public GameObject wonGameScreen;
    public static int checkpoint = 0;

    public Dictionary<string, object> trail; 
    public NewQuizManager newQuizManager;
    void Awake()
    {
        //checkpoint = PlayerPrefs.GetInt("Checkpoint", 0);
        checkpoint = 0; 
    }
    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public bool alive(){
        return isAlive;
    }
    public void gameOver()
    {
        Debug.Log("Game Over!");
        gameOverScreen.SetActive(true);
        isAlive = false;
    }
    public void gameWon(){
        isAlive = false;
        wonGameScreen.SetActive(true);
        if (TrailManager.trailDict == null)
        {
            Debug.LogError("TrailManager.trailDict is null! Make sure it's initialized.");
            return;
        }

        trail = TrailManager.trailDict;
        trail["currentCheckpoint"] = Convert.ToInt32(trail["currentCheckpoint"]) + 1;
        updateCheckpoint(trail);
        //Debug.Log("Level Completed! Checkpoint: " + checkpoint);
    }

    public async void updateCheckpoint(Dictionary<string, object> trailDict){
        checkpoint++; 

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
                    Debug.Log(trailDict["trailID"]);
                    Debug.Log(trail["trailID"].ToString()); 
                    if (trail["trailID"].ToString().Equals(trailDict["trailID"]))
                    {
                        Debug.Log("found a match!");
                        // Step 2: Modify the field
                        int currentCP = Convert.ToInt32(trail["currentCheckpoint"]); 
                        trail["currentCheckpoint"] = currentCP + 1; // or whatever value you want
                        Debug.Log("updated"); 
                        // Step 3: Replace it back in the list
                        trailsList[i] = trail;
                        break;
                    }
                }
            }

            // Write updated list back
            await docRef.UpdateAsync("trails", trailsList);
        }
    }

    
}
