using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip hurtSound, coinSound, bonusSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found on Hero GameObject");
        }
    }

    public void PlayCoinSound(){
        PlaySound(coinSound);
    }
    public void PlayBonusSound(){
        PlaySound(bonusSound);
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void PlayerAttackSound()
    {
        PlaySound(attackSound);
    }

    public void PlayerHurtSound()
    {
        PlaySound(hurtSound);
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
