using System;
using Unity.VisualScripting;

[System.Serializable]

public class Ingredient{
    private String name;
    private int count;

    public Ingredient(String n, int c){
        name = n;
        count = c;
    }

    public void setName(String n){
        name = n;
    }

    public void setCount(int n){
        count = n;
    }

    public String getName(){
        return this.name;
    }

    public int getCount(){
        return this.count;
    }

}