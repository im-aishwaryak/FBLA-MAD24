using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;
using TMPro;

public class SnowballGenerator : MonoBehaviour
{
    public GameObject snowball;
    public float MinSpeed;
    public float MaxSpeed;
    public float currentSpeed;
    // public LogicScript logic;

    public float SpeedMultiplier;
    public TextMeshProUGUI distanceLeft;
    public LogicScript logic;

    public int shardsCollected = 0;
    void Start() {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector3(25, 0, 0);
        currentSpeed = MinSpeed;
        // if(Random.Range(0, 1) == 0 && shardsCollected < logic.getLevel()){
            //false: generate shard
            // generateShard();
        // } else {
            generatesnowball();
        // }
        
    }

    public void GenerateNextSnowballWithGap()
    {
        if(logic.alive()){
            float randomWait = Random.Range(0.1f, 1.2f);
            Invoke("generatesnowball", randomWait);
        }
        
    }

    // Method to generate the snowball
    public void generatesnowball() {
        GameObject snowballIns = Instantiate(snowball, transform.position, transform.rotation);
        snowballIns.GetComponent<SnowballScript>().snowballGenerator = this;
    }

    // public void generateShard(){
    //     GameObject snowballIns = Instantiate(snowball, transform.position, transform.rotation);
    //     snowballIns.GetComponent<SnowballScript>().snowballGenerator = this;
    // }

    // Update is called once per frame
    void Update() {
        if (currentSpeed < MaxSpeed) {
            currentSpeed += SpeedMultiplier;
        } else if (currentSpeed >= MaxSpeed) {
            currentSpeed = 0;
            //end game
            logic.gameWon();
            distanceLeft.text = "Distance left: 0 ft";
            Time.timeScale = 0f;
            
        }

        if(logic.alive()){
            distanceLeft.text = "Distance left: " + ((MaxSpeed - currentSpeed)*10) + " ft";
        }

    }
}
