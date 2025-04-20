using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ingredientScript : MonoBehaviour
{
    // public SnowballGenerator snowballGenerator;
    public LogicScript logic;
    public  SpriteRenderer spriteRenderer;
    public Sprite[] ingredientSprites;

    // Start is called before the first frame update
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spriteRenderer.sprite = ingredientSprites[Random.Range(0, ingredientSprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        // Move the snowball to the left based on the current speed
        if(logic.alive()){
            transform.Translate(Vector2.left * 3 * Time.deltaTime);
        }
        if(transform.position.x < -14){
            Destroy(gameObject);
        }
    }

    // Trigger event when the snowball collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision object has the tag "nextLine"
        if (collision.gameObject.CompareTag("nextLine"))
        {
            // ingredientGenerator.GenerateNextSnowballWithGap(); // Calls generatesnowball method
        }

        // if (collision.gameObject.CompareTag("Finish"))
        // {
        //     Destroy(this.gameObject);
        // }
    }

    
}