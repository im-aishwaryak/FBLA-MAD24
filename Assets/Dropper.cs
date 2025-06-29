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
        //testing
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
    }
}
