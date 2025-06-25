using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    private AudioSource bgmSource;

    [System.Serializable]
    public class SceneTrack
    {
        public string sceneName;
        public AudioClip musicClip;
    }

    [Header("Scene-based music mapping")]
    public List<SceneTrack> sceneTracks;

    private string currentScene = "";
    private AudioClip currentClip = null;

    public bool isMuted = false; // Current mute state

    public void ToggleMusic()
    {
        isMuted = !isMuted;
        bgmSource.mute = isMuted;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            bgmSource = GetComponent<AudioSource>();
            bgmSource.loop = true;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        SceneTrack matched = sceneTracks.Find(s => s.sceneName == currentScene);

        if (matched != null && matched.musicClip != currentClip)
        {
            currentClip = matched.musicClip;
            bgmSource.clip = currentClip;
            bgmSource.Play();
        }
        else if (matched == null)
        {
            // No track for this scene, optional: stop music
            bgmSource.Stop();
            currentClip = null;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
