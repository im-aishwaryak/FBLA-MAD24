using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class shipPotion : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public PotionGameScript logic;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(this.name);
        logic.shipPotion();
        
    }
}
