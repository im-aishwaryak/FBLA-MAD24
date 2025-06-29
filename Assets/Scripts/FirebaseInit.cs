using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;

public class FirebaseInit : MonoBehaviour
{
    FirebaseFirestore firestore;

    // Event to notify when Firebase is ready
    public static System.Action OnFirebaseReady;
    public static bool IsFirebaseReady { get; private set; } = false;

    void Awake()
    {
        if (FindObjectsOfType<FirebaseInit>().Length > 1)
        {
            Destroy(gameObject); // Avoid duplicates
            return;
        }
        DontDestroyOnLoad(gameObject);

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                var app = Firebase.FirebaseApp.DefaultInstance;
                Debug.Log("Firebase is ready to go!");

                // Initialize Firestore
                firestore = FirebaseFirestore.DefaultInstance;
                Debug.Log("Firestore is ready to go!");

                // Set ready flag and notify listeners
                IsFirebaseReady = true;
                OnFirebaseReady?.Invoke();
            }
            else
            {
                Debug.LogError("Could not resolve Firebase dependencies: " + dependencyStatus);
                IsFirebaseReady = false;
            }
        });
    }
}
