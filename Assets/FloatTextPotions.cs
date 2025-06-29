using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatTextPotions : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
    // public Transform canvasTransform;

    void Start()
    {
        text = GetComponent<Text>();
        // text.transform.SetParent(canvasTransform, true);
    }

    // Update is called once per frame
    void Update()
    {
        //move the text up a bit
        transform.Translate(Vector2.up * 70 * Time.deltaTime);

        //then change the opacity of the text
        
        Color color = text.color;//gets color
        if (color.a > 0)
        {
            color.a = color.a - .01f; //lowers opacity
            text.color = color;//returns the color
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
