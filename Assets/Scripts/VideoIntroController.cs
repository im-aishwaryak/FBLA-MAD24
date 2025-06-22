using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoIntroController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.playOnAwake = false;
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += SeekAndPlayFromStart;
    }

    void SeekAndPlayFromStart(VideoPlayer vp)
    {
        videoPlayer.frame = 0;
        videoPlayer.Play();
        StartCoroutine(EnsureStartFromBeginning());
    }

    IEnumerator EnsureStartFromBeginning()
    {
        // wait a frame to ensure Play registered
        yield return null;
        videoPlayer.time = 0;
        videoPlayer.frame = 0;
    }

}
