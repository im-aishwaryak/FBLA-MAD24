using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour, IPointerClickHandler
{
    public PotionGameScript logic;
    // Start is called before the first frame update
    void Start()
    {
        // logic = GetComponent<PotionGameScript>();
        logic = GameObject.Find("LogicObjectName").GetComponent<PotionGameScript>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began){
    //         logic.setSelectedIngredient(this.name);
    //         Debug.Log("Picked up this ingredient: " + this.name);
    //     }
    // }

    public void OnPointerClick(PointerEventData eventData)
    {
        logic.setSelectedIngredient(this.name);
        
    }


}
