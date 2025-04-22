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
    public TextMeshProUGUI errorText;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignUp()
    {
        string email = EmailField.text;
        string password = PasswordField.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                FirebaseException firebaseEx = task.Exception?.Flatten().InnerExceptions[0] as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string msg = GetErrorMessage(errorCode);
                Debug.Log(msg); 
                ShowError(msg);
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
                FirebaseException firebaseEx = task.Exception?.Flatten().InnerExceptions[0] as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string msg = GetErrorMessage(errorCode);
                ShowError(msg);
                return;
            }

            FirebaseUser user = task.Result.User;
            Debug.Log("Logged in as: " + user.Email);
            SceneManager.LoadScene("Home");
        });
    }

    void ShowError(string msg)
    {
           errorText.text = msg;
    }

    string GetErrorMessage(AuthError error)
    {
        switch (error)
        {
            case AuthError.EmailAlreadyInUse:
                return "This email is already registered.";
            case AuthError.InvalidEmail:
                return "Email is not valid.";
            case AuthError.WeakPassword:
                return "Password is too weak.";
            case AuthError.WrongPassword:
                return "Incorrect password.";
            case AuthError.UserNotFound:
                return "Account not found.";
            default:
                return "Unknown error occurred.";
        }
    }


}
