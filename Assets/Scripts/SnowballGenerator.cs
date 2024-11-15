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

    public float SpeedMultiplier;
    public TextMeshProUGUI distanceLeft;

    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector3(25, 0, 0);
        currentSpeed = MinSpeed;
        generatesnowball();
    }

    public void GenerateNextSnowballWithGap()
    {
        float randomWait = Random.Range(0.1f, 1.2f);
        Invoke("generatesnowball", randomWait);
    }

    // Method to generate the snowball
    public void generatesnowball()
    {
        GameObject snowballIns = Instantiate(snowball, transform.position, transform.rotation);

        snowballIns.GetComponent<SnowballScript>().snowballGenerator = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpeed < MaxSpeed)
        {
            currentSpeed += SpeedMultiplier;
        } else if (currentSpeed >= MaxSpeed)
        {
            currentSpeed = 0;
        }

        distanceLeft.text = "Distance left - " + ((MaxSpeed - currentSpeed)*10) + " ft";

    }
}
