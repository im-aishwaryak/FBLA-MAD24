using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class gameLogicData : MonoBehaviour{
    
    public static gameLogicData Instance; // Singleton reference

    public int Coins = 0;
    public string selectedSubject;
    public bool gamePaused = false;
    private int ordersTaken = 0;
    private Dictionary <string, int> inventory = new Dictionary<string, int>(){
        {"Goldberry", 5},
        {"Flareberry", 5},
        {"Thorneberry", 6}
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
        inventory[berry] -= 1;
        // return 0;
    }

}