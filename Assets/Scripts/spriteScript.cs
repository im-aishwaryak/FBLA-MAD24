using UnityEngine;

public class spriteScript : MonoBehaviour
{
    public Rigidbody2D body;
    public float jumpStrength;
    public float moveStrength;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    body.velocity = Vector2.up * jumpStrength;
        //}

    }
}
