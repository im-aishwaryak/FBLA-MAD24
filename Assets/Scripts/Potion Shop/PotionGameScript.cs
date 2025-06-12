using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;
using TMPro;
using UnityEditor.UI;



public class PotionGameScript : MonoBehaviour
{

    //text variables
    public Text countDown;//timer text
    public Text orderCounter;//number of orders taken
    public Text Order1;
    public Text Order2;
    public Text Order3;
    public Text TBCount;//Thorneberrycount text
    public Text FBCount;//FlareberryCount text
    public Text GBCount;//Goldberry text


    //variables in game
    private float timer = 60f;
    private int ordersTaken = 0;
    // private float price;//updates current prices
    private int i1 = 0;//index for first order
    private int i2 = 0;//index for second order
    private int i3 = 0;//index for third order

    private string SelectedIngredient;

    private Potion[] potions = 
                            {new Potion("Brew of Serenity", "Goldberry", 5, "Thorneberry", 4),
                             new Potion("Earthroot Draught", "Thorneberry", 6, "Flareberry", 3),
                             new Potion("Springbrew Tonic", "Goldberry", 6, "Flareberry", 4),
                             new Potion("Fortune Serum", "Goldberry", 9, "Flareberry", 3),
                             new Potion("Bloodfire Tonic", "Thorneberry", 8, "Flareberry", 15),
                            };

    public Sprite[] ingredients;
    public Sprite[] potionImgs;
    public Image imageSlot1;
    public Image imageSlot2;


    // Start is called before the first frame update
    void Start()
    {
        //start timer
        timer = 60f;//this starts the timer
        spawnOrder(Order1);
        spawnOrder(Order2);
        spawnOrder(Order3);

        imageSlot1.enabled = false;
        imageSlot2.enabled = false;

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


        //updates the count of the inventory
        // Debug.Log("Is gameLogicData null? "+ (gameLogicData.Instance == null));
        // Debug.Log("Is FBCount null? " + (FBCount == null));
        FBCount.text = gameLogicData.Instance.getFlareberry() + "";
        GBCount.text = gameLogicData.Instance.getGoldBerry() + "";
        TBCount.text = gameLogicData.Instance.getThorneBerries() + "";
        
    }

    //updates the order for the given item
    private void spawnOrder(Text textBox){
        //pick a random number from 0 to potionLength-1
        int i = Random.Range(0, potions.Length-1);

        //save the potion for it
        Potion potion = potions[i];

        //update text box order
        textBox.text = potion.getName() + ":\n" 
                     + potion.getI1Name() + " $" + potion.getI1Price() + " (x" + potion.getI1Count() + ")\n"
                     + potion.getI2Name() + " $" + potion.getI2Price() + " (x" + potion.getI2Count() + ")";


        //return the potion
    }

    public void setSelectedIngredient(string str){
        SelectedIngredient = str;
        Debug.Log(SelectedIngredient);

    }

    public void dropAtLocation(string location){
        Debug.Log("Bob");
        if(SelectedIngredient != null && gameLogicData.Instance.getCount(SelectedIngredient) > 0){
            //success
            Debug.Log("Success");

            //subtract one from the count of the item
            gameLogicData.Instance.decrementBerry(SelectedIngredient);
            int i = 0;//i stands for index

            if(SelectedIngredient.Equals("Goldberry")){
                i = 0;
            } else if(SelectedIngredient.Equals("Flareberry")){
                i = 2;
            } else if(SelectedIngredient.Equals("Thorneberry")){
                i = 1;
            }

            //set the component to have that item visible
            if(location.Equals("i1Space") || location.Equals("i1Image")){
                //if there already is an image, increment that thing
                
                Debug.Log("in image");
                imageSlot1.sprite = ingredients[i];
                imageSlot1.enabled = true;
            } else {
                Debug.Log("in image setter 2");
                imageSlot2.sprite = ingredients[i];
                imageSlot2.enabled = true;
            }
            // imageSlot.sprite = ingredients[index]
            // imageSlot.enabled = true;
        }
    }
}
