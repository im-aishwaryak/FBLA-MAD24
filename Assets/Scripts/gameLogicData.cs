using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;


public class gameLogicData : MonoBehaviour{
    
    public static gameLogicData Instance; // Singleton reference

    public int Coins = 0;
    public string selectedSubject;
    public bool gamePaused = false;
    private int ordersTaken = 0;
    private Dictionary <string, int> inventory = new Dictionary<string, int>(){
        {"Goldberry", 20},
        {"Flareberry", 20},
        {"Thorneberry", 20}
    };


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate
        }
    }
    public void loseStuff(){
        var keys = new List<string>(inventory.Keys);
        
        foreach (var key in keys)
        {
            if(inventory[key] > 0){
                inventory[key] -=1;
            }
        }
    }

    public void incrementBerry(string berryName){
        inventory[berryName] += 1;
    }

    public Dictionary<string, int> getInventory(){
        return inventory;
    }

    public int getThorneBerries(){
        return inventory["Thorneberry"];
    }

    public int getFlareberry(){
        return inventory["Flareberry"];
    }

    public int getGoldBerry(){
        return inventory["Goldberry"];
    }

    public void incrementOrderCount(){
        ordersTaken++;
    }

    public int getCount(string berry){
        if(berry.Equals("Goldberry")){
            return getGoldBerry();
        } else if (berry.Equals("Flareberry")){
            return getFlareberry();
        } else if (berry.Equals("Thorneberry")){
            return getThorneBerries();
        }
        return 0;
    }

    public void decrementBerry(string berry){
        foreach (var key in inventory.Keys)
        {  
            Debug.Log("Key in dictionary: '" + key + "' (length: " + key.Length + ")");
            Debug.Log("Key bytes: " + System.Text.Encoding.UTF8.GetBytes(key));
        }
        this.inventory[berry] -= 1;
        // return 0;
    }

    public void UploadInventoryToFirestore()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        if (auth.CurrentUser != null)
        {
            string email = auth.CurrentUser.Email;

            // Convert inventory to Dictionary<string, object>
            Dictionary<string, object> inventoryData = new Dictionary<string, object>();
            foreach (KeyValuePair<string, int> entry in inventory)
            {
                inventoryData[entry.Key] = entry.Value;
            }

            DocumentReference docRef = db.Collection("users").Document(email);
            Dictionary<string, object> updateData = new Dictionary<string, object>
        {
            { "inventory", inventoryData }
        };

            docRef.SetAsync(updateData, SetOptions.MergeAll).ContinueWithOnMainThread(task => {
                if (task.IsCompleted && !task.IsFaulted)
                {
                    Debug.Log("Inventory successfully uploaded to Firestore.");
                }
                else
                {
                    Debug.LogError("Failed to upload inventory: " + task.Exception);
                }
            });
        }
        else
        {
            Debug.LogError("No authenticated user found.");
        }
    }

    public void incrementBerry(string berryName, int count){
        inventory[berryName] += count;
    }

    public void incrementCoins(int count){
        Coins += count;
    }

    public int getOrdersTaken(){
        return ordersTaken;
    }


}