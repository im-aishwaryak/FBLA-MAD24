using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    public LogicScript logic;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip _sound, bool loop = false)
    {
        source.loop = loop;
        source.clip = _sound;
        source.Play();
    }


}