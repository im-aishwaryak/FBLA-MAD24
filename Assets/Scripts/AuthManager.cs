using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Firestore;


public class AuthManager : MonoBehaviour
{
    FirebaseAuth auth;
    [Header("UI References")]
    public TMP_InputField EmailField;
    public TMP_InputField PasswordField;
    public TextMeshProUGUI errorText;
    public GameObject ErrorBox;

    private bool isFirebaseReady = false;

    void Start()
    {
        if (FirebaseInit.IsFirebaseReady)
        {
            OnFirebaseReady();
        }
        else
        {
            // Subscribe to Firebase ready event
            FirebaseInit.OnFirebaseReady += OnFirebaseReady;
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from event to prevent memory leaks
        FirebaseInit.OnFirebaseReady -= OnFirebaseReady;
    }

    void OnFirebaseReady()
    {
        auth = FirebaseAuth.DefaultInstance;
        isFirebaseReady = true;

        if (auth == null)
        {
            Debug.LogError("Firebase Auth is still null after initialization!");
            return;
        }

        Debug.Log("AuthManager: Firebase Auth is ready!");
    }

        public void SignUp()
    {


        if (!isFirebaseReady)
        {
            ShowError("Please wait, Firebase is initializing...");
            return;
        }

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
            addToDataBase(newUser); 
            SceneManager.LoadScene("Intro");
        });
    }

    public void Login()
    {


        if (!isFirebaseReady)
        {
            ShowError("Please wait, Firebase is initializing...");
            return;
        }

        if (auth == null)
        {
            Debug.LogError("Firebase Auth is not initialized!");
            ShowError("Authentication service unavailable");
            return;
        }
        Debug.Log("lol");
        string email = EmailField.text;
        string password = PasswordField.text;
        if(EmailField == null || PasswordField == null)
        {
            Debug.Log("Email: " + email);
            Debug.Log("Password: " + password); 
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            Debug.Log("works here"); 
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.Log("WHATTT"); 
                FirebaseException firebaseEx = task.Exception?.Flatten().InnerExceptions[0] as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string msg = GetErrorMessage(errorCode);
                ShowError(msg);
                return;
            }
            Debug.Log("no issue!"); 
            FirebaseUser user = task.Result.User;
            Debug.Log("Logged in as: " + user.Email);
            SceneManager.LoadScene("Home");
        });
    }

    public void LoginWithGoogle()
    {

    }

    void ShowError(string msg)
    {
        
        errorText.text = msg;
        ErrorBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(HideErrorAfterSeconds(3f));
    }

    IEnumerator HideErrorAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        ErrorBox.SetActive(false);
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
                return "Incorrect username or password.";
        }
    }

    void addToDataBase(FirebaseUser user)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(user.Email);

        Dictionary<string, object> userData = new Dictionary<string, object> {
            { "shopDaysOpen", 1 },
            { "currentProfit", 0 },
            { "inventory", new Dictionary<string, object>() },
            { "orders", new Dictionary<string, object>() },
            { "trails", new List<Dictionary<string, object>>()  },
            {"level", 1 },
            {"potionsSold", 0 },
            {"potionsInStock", 0 }
    }; 

        docRef.SetAsync(userData).ContinueWithOnMainThread(initTask =>
        {
            if (initTask.IsCompleted)
            {
                Debug.Log("User Firestore document created.");
            }
            else
            {
                Debug.LogError("Failed to create Firestore doc: " + initTask.Exception);
            }
        });

    }


}
