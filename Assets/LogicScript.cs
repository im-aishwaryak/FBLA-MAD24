using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    // Start is called before the first frame update
    public int score = 0;
    private bool isAlive = true;
    public GameObject gameOverScreen;

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    public void gameOver(){
        gameOverScreen.SetActive(true);
        isAlive = false;
    }

    public bool alive(){
        return isAlive;
    }
}
