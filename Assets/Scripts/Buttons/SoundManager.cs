using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource[] allAudioSources;
    private bool isSoundOn;

    void Awake()
    {
        // Ensure only one instance of SoundManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Load sound settings
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        allAudioSources = FindObjectsOfType<AudioSource>();
        UpdateSoundSettings();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        UpdateSoundSettings();
    }

    public bool IsSoundOn()
    {
        return isSoundOn;
    }

    private void UpdateSoundSettings()
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = !isSoundOn;
        }
    }
}
