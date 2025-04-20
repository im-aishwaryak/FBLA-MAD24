using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class gameLogicData : MonoBehaviour{
    
    public static gameLogicData Instance; // Singleton reference

    public int playerScore;
    public string selectedSubject;
    public bool gamePaused = false;    private Dictionary <string, int> inventory = new Dictionary<string, int>(){
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
        foreach (KeyValuePair<string, int> ingredient in inventory)
        {
            if(ingredient.Value > 0){
                inventory[ingredient.Key] -=1;
            } else {
                
            }
            
        }
    }

}