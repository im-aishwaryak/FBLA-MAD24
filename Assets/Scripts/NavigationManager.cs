using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NavigationManager : MonoBehaviour
{
    public void GoToBackpack()
    {
        SceneManager.LoadScene("Backpack");
    }

    public void GoToDashboard()
    {
        SceneManager.LoadScene("Dashboard");
    }

    public void GoToDinoRun()
    {
        SceneManager.LoadScene("DinoRun");
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void GoToIntro()
    {
        SceneManager.LoadScene("Intro");
    }

    public void GoToLibrary()
    {
        SceneManager.LoadScene("Library");
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene("Login");
    }

    public void GoToPotions()
    {
        SceneManager.LoadScene("Potions");
    }

    public void GoToPotionShop()
    {
        SceneManager.LoadScene("PotionShop");
    }

    public void GoToProgress()
    {
        SceneManager.LoadScene("Progress");
    }

    public void GoToProgressTracking()
    {
        SceneManager.LoadScene("ProgressTracking");
    }

    public void GoToQuiz()
    {
        SceneManager.LoadScene("Quiz");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GoToUserGuide()
    {
        SceneManager.LoadScene("UserGuide");
    }

    public void GoToUserTrails()
    {
        SceneManager.LoadScene("UserTrails");
    }

    public void GoToViewQuestions()
    {
        SceneManager.LoadScene("ViewQuestions");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
