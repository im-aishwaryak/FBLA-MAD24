using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SnowballScript : MonoBehaviour
{
    public SnowballGenerator snowballGenerator;
    public LogicScript logic;
    void Start(){
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    

    // Update is called once per frame
    void Update()
    {
        // Move the snowball to the left based on the current speed
        if(logic.alive()){
            transform.Translate(Vector2.left * snowballGenerator.currentSpeed * Time.deltaTime);
        }
        if(transform.position.x < -14){
            Destroy(this.gameObject);
        }
    }

    // Trigger event when the snowball collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log()
            gameLogicData.Instance.loseStuff();
            Destroy(this.gameObject);
        }
        
        // Check if the collision object has the tag "nextLine"
        if (collision.gameObject.CompareTag("nextLine"))
        {
            snowballGenerator.GenerateNextSnowballWithGap(); // Calls generatesnowball method
        }

        


    }
}
