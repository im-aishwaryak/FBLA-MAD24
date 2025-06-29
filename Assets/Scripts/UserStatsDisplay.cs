using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;

public class UserStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI potionsSoldText;
    public TextMeshProUGUI currentBalanceText;
    public TextMeshProUGUI potionsInStockText;
    public TextMeshProUGUI trailsCompletedText;

    public TextMeshProUGUI Flareberry;
    public TextMeshProUGUI Goldberry;
    public TextMeshProUGUI Thorneberry;

    private FirebaseAuth auth;
    private FirebaseFirestore db;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;

        if (auth.CurrentUser != null)
        {
            Debug.Log("Loading stats for: " + auth.CurrentUser.Email);
            LoadUserStats(auth.CurrentUser.Email);
        }
        else
        {
            Debug.LogError("No logged in user found.");
        }
    }

    void LoadUserStats(string userEmail)
    {
        DocumentReference docRef = db.Collection("users").Document(userEmail);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && task.Result.Exists)
            {
                DocumentSnapshot snapshot = task.Result;
                Dictionary<string, object> userData = snapshot.ToDictionary();

                Debug.Log("User data loaded.");

                // 1. Profit
                if (userData.TryGetValue("currentProfit", out object profit))
                {
                    currentBalanceText.text = $"${profit}";
                    Debug.Log("Profit: " + profit);
                }

                // 2. Trails Completed
                if (userData.TryGetValue("trailsCompleted", out object trailsObj) && trailsObj is List<object> trails)
                {
                    trailsCompletedText.text = $"{trails.Count}";
                    Debug.Log("Trails completed: " + trails.Count);
                }

                // 3. Inventory
                if (userData.TryGetValue("inventory", out object inventoryObj) && inventoryObj is Dictionary<string, object> inventory)
                {
                    Debug.Log("Inventory found.");

                    int totalPotions = 0;
                    foreach (var entry in inventory)
                    {
                        Debug.Log($"Inventory item: {entry.Key} = {entry.Value} ({entry.Value?.GetType()})");

                        if (entry.Value is long amount)
                        {
                            totalPotions += (int)amount;
                        }
                        else if (entry.Value is double d)
                        {
                            totalPotions += (int)d;
                        }
                        else
                        {
                            Debug.LogWarning($"Unexpected type in inventory for {entry.Key}: {entry.Value?.GetType()}");
                        }
                    }

                    potionsInStockText.text = $"{totalPotions}";

                    Flareberry.text = $"{GetCount(inventory, "Flareberry")}";
                    Goldberry.text = $"{GetCount(inventory, "Goldberry")}";
                    Thorneberry.text = $"{GetCount(inventory, "Thorneberry")}";
                }
                else
                {
                    Debug.LogWarning("Inventory not found or could not be cast.");
                }

                // 4. Shop Days Open
                if (userData.TryGetValue("potionsSold", out object daysOpen))
                {
                    potionsSoldText.text = $"{daysOpen}";
                    Debug.Log("Shop days open: " + daysOpen);
                }
            }
            else
            {
                Debug.LogError("Failed to load user data or document does not exist.");
            }
        });
    }

    int GetCount(Dictionary<string, object> inventory, string key)
    {
        if (!inventory.TryGetValue(key, out object val))
        {
            Debug.LogWarning($"Key '{key}' not found in inventory.");
            return 0;
        }

        if (val is long longVal)
        {
            Debug.Log($"{key} (long): {longVal}");
            return (int)longVal;
        }

        if (val is double doubleVal)
        {
            Debug.Log($"{key} (double): {doubleVal}");
            return (int)doubleVal;
        }

        Debug.LogWarning($"{key} value is of unexpected type: {val?.GetType()}");
        return 0;
    }
}
