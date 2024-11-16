using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int score = 0;
    private bool isAlive = true;
    public GameObject gameOverScreen;
    public GameObject wonGameScreen;
    public int level = 0;

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    public void gameOver(){
        Debug.Log("Game Over!");
        gameOverScreen.SetActive(true);
        isAlive = false;
    }

    public bool alive(){
        return isAlive;
    }

    public void gameWon(){
        isAlive = false;
        wonGameScreen.SetActive(true);
        level ++;
        Debug.Log("Level Completed!");
    }

    public void nextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int getLevel(){
        return level;
    }
}
