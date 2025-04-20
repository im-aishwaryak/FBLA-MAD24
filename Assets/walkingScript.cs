using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingScript : MonoBehaviour
{
    public Sprite[] walkingSprites;
    public  SpriteRenderer spriteRenderer;
    private int index = 0;
    private float changeTime = 0.2f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = walkingSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < changeTime){
            timer += Time.deltaTime;
        } else {
            if(index < walkingSprites.Length-1){
                index ++;
            } else {
                index = 0;
            }
            spriteRenderer.sprite = walkingSprites[index];
            timer = 0;
        }
    }
}
