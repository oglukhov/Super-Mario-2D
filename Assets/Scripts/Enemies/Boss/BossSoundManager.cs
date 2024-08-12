using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip bossDead, throwStone, bossHurt;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHurt(){
        PlaySound(bossHurt);
    }
    public void PlayThrow(){
        PlaySound(throwStone);
    }
    public void PlayDead(){
        PlaySound(bossDead);
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
