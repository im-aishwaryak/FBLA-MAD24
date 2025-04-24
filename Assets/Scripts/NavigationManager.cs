using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NavigationManager : MonoBehaviour
{
    public void GoToHome()
    {
        SceneManager.LoadScene("Home"); 
    }
    public void ResumeJourney()
    {
        SceneManager.LoadScene("Quiz"); 
    }

    public void GoToTrails()
    {
        SceneManager.LoadScene("UserTrails"); 
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("Dashboard"); 
    }

    public void GoToLibrary()
    {
        SceneManager.LoadScene("Library"); 
    }

    public void GoToUserGuide()
    {
        Application.OpenURL("https://snowpeak-secrets.netlify.app/instructions.html");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings"); 
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene("Login");
    }

    public void GoToPotions()
    {
        SceneManager.LoadScene("Potions"); 
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }

}
