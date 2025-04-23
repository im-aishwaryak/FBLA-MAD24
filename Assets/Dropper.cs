using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropper : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public PotionGameScript logic;
    void Start()
    {
        // logic = GetComponent<PotionGameScript>();
        logic = GameObject.Find("LogicObjectName").GetComponent<PotionGameScript>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(this.name);
            logic.dropAtLocation(this.name);

        // Debug.Log("dropped at this space: " + this.name);
        // if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began){
        //     Debug.Log("dropped at this space: " + this.name);
        //     // logic.setSelectedIngredient(this.name);
            
        // }
    }
}
