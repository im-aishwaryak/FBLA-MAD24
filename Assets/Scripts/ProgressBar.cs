using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image fillImage; // Assign ProgressBarFill here in Inspector

    // Call this function with a value between 0 and 1 (e.g., 0.5f for 50%)
    public void SetProgress(float percent)
    {
        fillImage.fillAmount = Mathf.Clamp01(percent);
    }

    public void SetInactive()
    {
        fillImage.fillAmount = 1f;          // Full bar
        fillImage.color = Color.gray;       // Change to gray color
    }

    void Start()
    {
        // 👇 Test the function by setting progress to 50%
        SetInactive();
    }
}