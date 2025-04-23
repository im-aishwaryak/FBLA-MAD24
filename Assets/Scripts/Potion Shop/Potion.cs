using System;

[System.Serializable]
public class Potion
{
    public string name;
    public string ingredient1Name;
    public string ingredient2Name;
    public float sellingPrice;
    public int ingredient1Count;
    public int ingredient2Count;


    public Potion(String name, String i1, int i1Count, String i2, int i2Count, float sellingPrice)
    {
        this.name = name;
        ingredient1Count = i1Count;
        ingredient1Name = i1;
        ingredient2Name = i2;
        ingredient2Count = i2Count;
        this.sellingPrice = sellingPrice;
    }

    public String getName(){
        return this.name;
    }

    public float getPrice(){
        return this.sellingPrice;
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

}