using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;
using TMPro;
using UnityEditor.UI;
using System.Collections.Generic;



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
    public Text slot1Txt;//slot 1 count of item
    public Text slot2Txt;//slot 2 count of item


    //variables in game
    private float timer = 60f;
    private int ordersTaken = 0;
    // private float price;//updates current prices
    private int i1 = 0;//index for first order
    private int i2 = 0;//index for second order
    private int i3 = 0;//index for third order


    public Sprite[] ingredients;
    public Sprite[] potionImgs;
    public Image imageSlot1;
    public Image imageSlot2;



    private string SelectedIngredient;

    private Potion[] potions = 
                            {new Potion("Brew of Serenity", "Goldberry", 5, "Thorneberry", 4),
                             new Potion("Earthroot Draught", "Thorneberry", 6, "Flareberry", 3),
                             new Potion("Springbrew Tonic", "Goldberry", 6, "Flareberry", 4),
                             new Potion("Fortune Serum", "Goldberry", 9, "Flareberry", 3),
                             new Potion("Bloodfire Tonic", "Thorneberry", 8, "Flareberry", 15),
                            };

    
    public string iSlot1;//ingredient in slot 1
    public string iSlot2;//ingredient in slot 2

    // private System.Collections.Generic.Dictionary <string, int> inventory = new Dictionary<string, int>(){
    //     {},
    //     {"", 0}
    // };

    private Ingredient[] slots = {new Ingredient("", 0), new Ingredient("", 0)};

    // Start is called before the first frame update
    void Start()
    {
        //start timer
        timer = 60f;//this starts the timer
        i1 = spawnOrder(Order1);
        i2 = spawnOrder(Order2);
        i3 = spawnOrder(Order3);

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

        slot1Txt.text = slots[0].getCount() + "";
        slot2Txt.text = slots[1].getCount() + "";
        
    }

    //updates the order for the given item
    private int spawnOrder(Text textBox){
        //pick a random number from 0 to potionLength-1
        int i = Random.Range(0, potions.Length-1);

        //save the potion for it
        Potion potion = potions[i];

        //update text box order
        textBox.text = potion.getName() + ":\n" 
                     + potion.getI1Name() + " $" + potion.getI1Price() + " (x" + potion.getI1Count() + ")\n"
                     + potion.getI2Name() + " $" + potion.getI2Price() + " (x" + potion.getI2Count() + ")";


        return i;
    }

    public void setSelectedIngredient(string str){
        SelectedIngredient = str;
        Debug.Log(SelectedIngredient);

    }

    public void dropAtLocation(string location){
        // Debug.Log("Bob");
        if(SelectedIngredient != null && gameLogicData.Instance.getCount(SelectedIngredient) > 0){
            //success

            //subtract one from the count of the item
            Debug.Log("Selected ingredient: '" + SelectedIngredient + "'");
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
                //check if the first slot has not ingredient saved
                if(slots[0].getName() == null || slots[0].getName().Equals("")){
                    Debug.Log("iSlot1 is null");

                    //this means that this is the first ingredient that is being placed
                    slots[0].setName(SelectedIngredient);//updates the name of the ingredient
                    slots[0].setCount(1);

                } else if (slots[0].getName().Equals(SelectedIngredient)){
                    //this means that the ingredient is the same
                    slots[0].setCount(slots[0].getCount()+1);//increment the count
                } else if (!slots[0].getName().Equals(SelectedIngredient)){
                    //this means that the ingredient has changed.

                    int tempCount = slots[0].getCount();
                    string name = slots[0].getName();

                    //update the inventory to have those berries
                    gameLogicData.Instance.incrementBerry(name, tempCount);

                    //update the actual thing to be 1 with the new name
                    slots[0].setName(SelectedIngredient);
                    slots[0].setCount(1);
                }

                
                Debug.Log("in image");
                imageSlot1.sprite = ingredients[i];
                imageSlot1.enabled = true;
            } else {
                //check if the first slot has not ingredient saved
                if(slots[1].getName() == null || slots[1].getName().Equals("")){
                    // Debug.Log("iSlot1 is null");

                    //this means that this is the first ingredient that is being placed
                    slots[1].setName(SelectedIngredient);//updates the name of the ingredient
                    slots[1].setCount(1);

                } else if (slots[1].getName().Equals(SelectedIngredient)){
                    //this means that the ingredient is the same
                    slots[1].setCount(slots[1].getCount()+1);//increment the count
                } else if (!slots[1].getName().Equals(SelectedIngredient)){
                    //this means that the ingredient has changed.

                    int tempCount = slots[1].getCount();
                    string name = slots[1].getName();

                    //update the inventory to have those berries
                    gameLogicData.Instance.incrementBerry(name, tempCount);

                    //update the actual thing to be 1 with the new name
                    slots[1].setName(SelectedIngredient);
                    slots[1].setCount(1);
                }
                Debug.Log("in image setter 2");
                imageSlot2.sprite = ingredients[i];
                imageSlot2.enabled = true;
            }
            // imageSlot.sprite = ingredients[index]
            // imageSlot.enabled = true;
        }
    }

    public void shipPotion(){
        //this is the potion that ships the potion

        //first you want to check if it is a valid potion

        //you can do this by looking through the orders, comparing the recipies of them to what they have
        if(checkPotion(i1)){
            //it matches potion 1
            calculateRewards(i1);
        } else if(checkPotion(i2)){
            //it matches potion 2
            calculateRewards(i2);
        } else if(checkPotion(i3)){
            //it matches potion 3
            calculateRewards(i3);
        }
        
        
        
    }

    private bool checkPotion(int i){
        // bool output = false;
        if(potions[i].getI1Name().Equals(slots[0].getName()) && potions[i].getI2Name().Equals(slots[1].getName()) && (potions[i].getI1Count() == slots[0].getCount() && potions[i].getI2Count() == slots[1].getCount())){
            return true; 
        } else if(potions[i].getI1Name().Equals(slots[1].getName()) && potions[i].getI2Name().Equals(slots[0].getName()) && (potions[i].getI1Count() == slots[1].getCount() && potions[i].getI2Count() == slots[0].getCount())){
            return true;
        }
        return false;
    }

    private void calculateRewards(int i){
        Potion potion = potions[i];

        gameLogicData.Instance.incrementCoins((int)potion.getSellingPrice());
        gameLogicData.Instance.incrementOrderCount();

        //reset stuff
        slots[0] = new Ingredient("", 0);
        slots[1] = new Ingredient("", 0);

        imageSlot1.enabled = false;
        imageSlot2.enabled = false;

        i1 = spawnOrder(Order1);
        i2 = spawnOrder(Order2);
        i3 = spawnOrder(Order3);
        
        timer = 60;
        orderCounter.text = "Orders Taken: " + gameLogicData.Instance.getOrdersTaken();

    }
}
