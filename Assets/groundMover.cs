using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundMover : MonoBehaviour
{
    // Start is called before the first frame update
    public LogicScript logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the ingredient based on speed
        if(logic.alive()){
            transform.Translate(Vector2.left * 4 * Time.deltaTime);
        }
        if(transform.position.x < -50){
            Destroy(gameObject);
        }
    }
}
