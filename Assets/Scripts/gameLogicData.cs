using System.Collections;
using System.Collections.Generic;

public class gameLogicData{
    private Dictionary <string, int> inventory = new Dictionary<string, int>();


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