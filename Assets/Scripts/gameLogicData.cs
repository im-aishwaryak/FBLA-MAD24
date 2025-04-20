using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class gameLogicData : MonoBehaviour{
    
    public static gameLogicData Instance; // Singleton reference

    public int playerScore;
    public string selectedSubject;
    public bool gamePaused = false;
    private Dictionary <string, int> inventory = new Dictionary<string, int>(){
        {"GoldBerry", 0},
        {"Raspberry", 0},
        {"ThorneBerry", 0}
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
            } else {
                
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
        return inventory["ThorneBerry"];
    }

    public int getRaspberry(){
        return inventory["Raspberry"];
    }

    public int getGoldBerry(){
        return inventory["GoldBerry"];
    }

}