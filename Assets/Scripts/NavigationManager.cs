using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NavigationManager : MonoBehaviour
{
    public void GoToProfile()
    {
        Debug.Log("working"); 
        SceneManager.LoadScene("ProfileScreen"); 
    }

    public void GoToTrail()
    {
        Debug.Log("kys"); 
        SceneManager.LoadScene("Quiz"); 
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("ShopScreen"); 
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
