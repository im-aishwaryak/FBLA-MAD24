using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;
using TMPro;



public class PotionGameScript : MonoBehaviour
{

    //text variables
    public Text countDown;//timer text
    public Text orderCounter;//number of orders taken
    public Text Order1;
    public Text Order2;
    public Text Order3;

//variables in game
    private float timer = 60f;
    private int ordersTaken = 0;

    private Potion[] potion = 
                            {new Potion("Brew of Serenity", "Goldberry", 5, "Thorneberry", 4, 59),
                             new Potion("Earthroot Draught", "Thorneberry", 6, "Flareberry", 3, 48),
                             new Potion("Springbrew Tonic", "Goldberry", 6, "Flareberry", 4, 58),
                             new Potion("Fortune Serum", "Goldberry", 9, "Flareberry", 3, 75),
                             new Potion("Bloodfire Tonic", "Thorneberry", 8, "Flareberry", 15, 108),
                            };



    // Start is called before the first frame update
    void Start()
    {
        //start timer
        timer = 60f;//this starts the timer
        spawnOrder(Order1);
        spawnOrder(Order2);
        spawnOrder(Order3);
        

    }

    // Update is called once per frame
    void Update() {
        if((timer - Time.deltaTime) > 0){
            timer -= (Time.deltaTime);//update timer
            countDown.text = Mathf.FloorToInt(timer) + "";//update text
        } else {
            //timer has run out!
            
            timer = 60f;//reset timer
            countDown.text = Mathf.FloorToInt(timer) + "";//update text
            //spawn new orders (below)
            spawnOrder(Order1);
            spawnOrder(Order2);
            spawnOrder(Order3);
        }
        
    }

    //updates the order for the given item
    private void spawnOrder(Text textBox){
        //pick a random number from 0 to potionLength-1
        int index = Random.Range(0, potion.Length-1);
        //find price



        //order
        textBox.text = "";
        //update orders
    }
}
