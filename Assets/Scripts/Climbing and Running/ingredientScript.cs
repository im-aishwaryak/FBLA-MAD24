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
    private int i = 0;//the index of the ingredient

    // Start is called before the first frame update
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        i = Random.Range(0, ingredientSprites.Length);
        spriteRenderer.sprite = ingredientSprites[i];
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

    
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log()
            if(i == 0){
                //it is a throneberry
                gameLogicData.Instance.incrementBerry("ThorneBerry");
            } else if (i == 1){
                //it is a raspberry
                gameLogicData.Instance.incrementBerry("Raspberry");
            } else {
                //it is a goldberry
                gameLogicData.Instance.incrementBerry("GoldBerry");

            }
            Destroy(this.gameObject);
        }


    }

    
}