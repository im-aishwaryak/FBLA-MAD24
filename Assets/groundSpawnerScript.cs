using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundSpawnerScript : MonoBehaviour
{
    public GameObject ground;
    private float timer = 0;
    private float spawnRate = 4;
    // Start is called before the first frame update
    void Start()
    {
        spawnGround();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnGround();
            timer = 0;
        }
    }
    
    void spawnGround(){
        Instantiate(ground, transform.position, transform.rotation);
        // Debug.Log("Spawn Thing");
    }
}
