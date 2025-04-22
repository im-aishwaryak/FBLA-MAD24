using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    FirebaseAuth auth;
    [Header("UI References")]
    public TMP_InputField EmailField;
    public TMP_InputField PasswordField;
    void Start()
    {
        Debug.Log("hola");
        auth = FirebaseAuth.DefaultInstance;
        Debug.Log("hola");
    }

    public void SignUp()
    {
        string email = EmailField.text;
        string password = PasswordField.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Signup Failed: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.Log("User created: " + newUser.Email);
            SceneManager.LoadScene("Home");
        });
    }

    public void Login()
    {
        string email = EmailField.text;
        string password = PasswordField.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Login Failed: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result.User;
            Debug.Log("Logged in as: " + user.Email);
            SceneManager.LoadScene("Home");
        });
    }
}
