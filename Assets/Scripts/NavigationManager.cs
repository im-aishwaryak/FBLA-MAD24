using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Load the click sound from Resources/Sounds/correct
        clickSound = Resources.Load<AudioClip>("Sounds/click");
        if (clickSound == null)
        {
            Debug.LogWarning("Click sound not found");
        }
    }

    private IEnumerator PlayClickAndLoad(string sceneName)
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);

        yield return new WaitForSeconds(0.2f); // Short delay to hear sound
        SceneManager.LoadScene(sceneName);
    }

    public void GoToBackpack() => StartCoroutine(PlayClickAndLoad("Backpack"));
    public void GoToDashboard() => StartCoroutine(PlayClickAndLoad("Dashboard"));
    public void GoToDinoRun() => StartCoroutine(PlayClickAndLoad("DinoRun"));
    public void GoToHome() => StartCoroutine(PlayClickAndLoad("Home"));
    public void GoToIntro() => StartCoroutine(PlayClickAndLoad("Intro"));
    public void GoToLibrary() => StartCoroutine(PlayClickAndLoad("Library"));
    public void GoToLogin() => StartCoroutine(PlayClickAndLoad("Login"));
    public void GoToPotions() => StartCoroutine(PlayClickAndLoad("Potions"));
    public void GoToPotionShop() => StartCoroutine(PlayClickAndLoad("PotionShop"));
    public void GoToProgress() => StartCoroutine(PlayClickAndLoad("Progress"));
    public void GoToProgressTracking() => StartCoroutine(PlayClickAndLoad("ProgressTracking"));
    public void GoToQuiz() => StartCoroutine(PlayClickAndLoad("Quiz"));
    public void GoToSettings() => StartCoroutine(PlayClickAndLoad("Settings"));
    public void GoToUserGuide() => StartCoroutine(PlayClickAndLoad("UserGuide"));
    public void GoToUserTrails() => StartCoroutine(PlayClickAndLoad("UserTrails"));
    public void GoToViewQuestions() => StartCoroutine(PlayClickAndLoad("ViewQuestions"));
    public void QuitGame()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);

        Application.Quit();
    }
}
