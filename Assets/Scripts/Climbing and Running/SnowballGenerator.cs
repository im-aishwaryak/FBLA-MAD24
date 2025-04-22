using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;
using TMPro;

public class SnowballGenerator : MonoBehaviour
{
    public GameObject snowball;
    public float MinSpeed;
    // public float MaxSpeed;
    public float currentSpeed;
    // public LogicScript logic;

    public float SpeedMultiplier;
    public TextMeshProUGUI distanceLeft;
    public LogicScript logic;

    public int shardsCollected = 0;
    private float time = 55;
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
        Debug.Log("start");
        
    }

    public void GenerateNextSnowballWithGap()
    {
        if(logic.alive()){
            float randomWait = Random.Range(0.05f, 2f);
            Debug.Log("RandomWair: " + randomWait);
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
        
        if (time <= 0) {
            currentSpeed = 0;
            //end game
            logic.gameWon();
            distanceLeft.text = "Time Left: 0 seconds";
            Time.timeScale = 0f;
        }

        if(logic.alive()){
            // Debug.Log(time);
            time-=(Time.deltaTime);
            distanceLeft.text = "Time Left: " + Mathf.FloorToInt(time) + " seconds left";
        }

    }
}
