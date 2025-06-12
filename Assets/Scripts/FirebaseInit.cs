using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;

public class FirebaseInit : MonoBehaviour
{
    FirebaseFirestore firestore; 
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
                // Now you can safely use FirebaseAuth and other services

                firestore = FirebaseFirestore.DefaultInstance;
                Debug.Log("Firestore is ready to go!");
            }
            else
            {
                Debug.LogError("Could not resolve Firebase dependencies: " + dependencyStatus);
            }
        });
    }
}
