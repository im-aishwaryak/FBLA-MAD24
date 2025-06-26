using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int score = 0;
    private bool isAlive = true;
    public GameObject gameOverScreen;
    public GameObject wonGameScreen;
    public static int checkpoint = 0;

    public QuizManager quizManager;

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public bool alive(){
        return isAlive;
    }
    public void gameOver()
    {
        Debug.Log("Game Over!");
        gameOverScreen.SetActive(true);
        isAlive = false;
    }
    public void gameWon(){
        isAlive = false;
        wonGameScreen.SetActive(true);
        checkpoint++;
        Debug.Log("Level Completed! Checkpoint: " + checkpoint);
    }

    public int getCheckpoint(){
        return checkpoint;
    }
}
