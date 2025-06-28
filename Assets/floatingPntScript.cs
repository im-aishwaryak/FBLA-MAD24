using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingPntScript : MonoBehaviour
{

    // Start is called before the first frame update
    TextMesh text;

    void Start()
    {
        text = GetComponent<TextMesh>();
        if (text.text.Contains("-"))
        {
            text.color = Color.red;
        }
        else if (text.text.Contains("+"))
        {
            text.color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move the text up a bit
        transform.Translate(Vector2.up * 1 * Time.deltaTime);

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
