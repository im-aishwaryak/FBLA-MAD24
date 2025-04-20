using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;
using TMPro;
public class IngredientSpawnScript : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject ingredient;
    public float spawnRate = 2;
    private float timer = 0;

    private float heightOffset = 2.4f;
    public TextMeshProUGUI inventCount;
    void Start()
    {
        spawnIngredient();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate){
            timer += Time.deltaTime;
        } else {
            spawnIngredient();
            if(spawnRate !=0){
                spawnRate = Random.Range(0, 5);
            } else {
                spawnRate = Random.Range(3,5);
            }
            timer = 0;
        }

        //update count
        updateCount();
    }

    void updateCount(){
        inventCount.text = gameLogicData.Instance.getGoldBerry() + "\n" + gameLogicData.Instance.getThorneBerries() + "\n" + gameLogicData.Instance.getRaspberry() + "\n";
    }



    void spawnIngredient(){
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(ingredient, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
