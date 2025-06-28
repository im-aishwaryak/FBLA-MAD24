using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class groundSpawnerScript : MonoBehaviour
{
    public GameObject ground;
    private float timer = 0;
    private float spawnRate = 6.30f;
    // Start is called before the first frame update
    void Start()
    {
        spawnGround(new Vector3(0, -3.9576f, -5f));
        spawnGround(new Vector3(25.22f, -3.9576f, -5f));
        spawnGround(new Vector3(50.44f, -3.9576f, -5f));
        
        // spawnGround(new Vector3(18.2f, -3.9576f, -5f));
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

    void spawnGround()
    {
        Instantiate(ground, new Vector3(50.44f,-3.9576f, -5f), transform.rotation);
        // Debug.Log("Spawn Thing");
    }

    void spawnGround(Vector3 pos)
    {
        /*GameObject temp = */Instantiate(ground, pos, transform.rotation);
        // SpriteRenderer sr = temp.GetComponent<SpriteRenderer>();
        // Debug.Log(sr.bounds.size);
    }
}
