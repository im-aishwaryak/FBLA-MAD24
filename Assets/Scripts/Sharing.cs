using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sharing : MonoBehaviour
{
    string filePath;

    public void CaptureScreenshot()
    {
        filePath = Application.persistentDataPath + "/ProgressScreenshot.png";
        ScreenCapture.CaptureScreenshot(filePath);
        Invoke("ShareScreenshot", 0.5f);  // Slight delay to ensure the screenshot is saved
    }

    public void ShareScreenshot()
    {
        // Using Native Share to share the screenshot
        new NativeShare().AddFile(filePath)
            .SetSubject("Check out my progress on SnowPeak Secrets!")
            .SetText("I just reached a new level on SnowPeak Secrets! Can you beat my score?")
            .Share();
    }
}

