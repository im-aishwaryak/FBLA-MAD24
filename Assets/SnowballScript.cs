using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SnowballScript : MonoBehaviour
{
    public SnowballGenerator snowballGenerator;
    public LogicScript logic;
    void start(){
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    

    // Update is called once per frame
    void Update()
    {
        // Move the snowball to the left based on the current speed
        transform.Translate(Vector2.left * snowballGenerator.currentSpeed * Time.deltaTime);
        
    }

    // Trigger event when the snowball collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision object has the tag "nextLine"
        if (collision.gameObject.CompareTag("nextLine"))
        {
            snowballGenerator.GenerateNextSnowballWithGap(); // Calls generatesnowball method
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
        }
    }
}
