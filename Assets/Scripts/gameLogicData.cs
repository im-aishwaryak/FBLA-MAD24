using System.Collections;
using System.Collections.Generic;

public class gameLogicData{
    private Dictionary <string, int> inventory = new Dictionary<string, int>();


    public void loseStuff(){
        foreach (string ingredient in inventory)
        {
            if(inventory[ingredient] > 0){
                inventory[ingredient] -=1;
            } else {
                
            }
            
        }
    }

}