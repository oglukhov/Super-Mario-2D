using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSoundManager : MonoBehaviour
{
    public AudioClip looseSound, victorySound, backSound, bossSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayBackSound(){
        PlaySound(backSound);
    }
    public void PlayBossSound(){
        PlaySound(bossSound);
    }

    public void PlayLooseSound(){
        PlaySound(looseSound);
    }
    public void PlayVictorySound(){
        PlaySound(victorySound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioClip is null");
        }
    }
}
