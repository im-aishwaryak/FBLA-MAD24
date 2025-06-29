using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shipPotion : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public PotionGameScript logic;
    


    void Start()
    {
        // logic = GetComponent<PotionGameScript>();
        Debug.Log("Initialized");
        logic = GameObject.Find("LogicObjectName").GetComponent<PotionGameScript>();
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(this.name);
        Debug.Log("is logic null: " + logic);
        logic.shipPotion();
        

        Debug.Log("Shipped!");
    }
}
