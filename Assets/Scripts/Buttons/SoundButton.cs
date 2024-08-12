using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public Sprite soundOnSprite; // Drag the sound on sprite here in the inspector
    public Sprite soundOffSprite; // Drag the sound off sprite here in the inspector

    private Button soundButton;
    private Image buttonImage;
    private Text buttonText;

    void Start()
    {
        soundButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        buttonText = soundButton.GetComponentInChildren<Text>();

        // Set initial button state
        UpdateButtonState();

        soundButton.onClick.AddListener(OnSoundButtonClick);
    }

    void OnSoundButtonClick()
    {
        SoundManager.instance.ToggleSound();
        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        if (SoundManager.instance.IsSoundOn())
        {
            buttonImage.sprite = soundOnSprite;
        }
        else
        {
            buttonImage.sprite = soundOffSprite;
        }
    }
}
