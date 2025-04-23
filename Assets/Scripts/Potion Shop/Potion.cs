using System;

[System.Serializable]
public class Potion
{
    public string name;
    public string ingredient1Name;
    public string ingredient2Name;
    // public float sellingPrice;
    public int ingredient1Count;
    public int ingredient2Count;


    public int i1price;
    public int i2price;


    public Potion(String name, String i1, int i1Count, String i2, int i2Count)
    {
        this.name = name;//saves name
        ingredient1Count = i1Count;//saves the # of ingredient 1
        ingredient1Name = i1;//saves the name of ingredient 1
        ingredient2Name = i2;//saves the name of ingredient 2
        ingredient2Count = i2Count;//saves the # of ingreident 2
        // this.sellingPrice = sellingPrice;

        i1price = updatePrice(ingredient1Name);
        i2price = updatePrice(ingredient2Name);

    }

    private int updatePrice(String name){
        if(name.Equals("Thorneberry")){
            return 6;
        } else if(name.Equals("Goldberry")){
            return 7;
        } else {
            return 4;//is flareberry
        }

    }

    public String getName(){
        return this.name;
    }

    public float getSellingPrice(){
        return ((this.getI1Count() * this.i1price) + (this.getI2Count() * this.i2price));
    }

    public int getI1Count(){
        return this.ingredient1Count;
    }

    public int getI2Count(){
        return this.ingredient2Count;
    }

    public String getI1Name(){
        return this.ingredient1Name;
    }

    public String getI2Name(){
        return this.ingredient2Name;
    }

    public int getI1Price(){
        return this.i1price;
    }

    public int getI2Price(){
        return this.i2price;
    }

}