using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] allButtonSounds;
    private bool isSoundOn = true;
    public Button soundToggleButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    private Image buttonImage;

    private void Start()
    {
        // Find all AudioSources in the scene (attached to buttons)
        allButtonSounds = FindObjectsOfType<AudioSource>();

        if (soundToggleButton != null)
        {
            buttonImage = soundToggleButton.GetComponent<Image>();
            soundToggleButton.onClick.AddListener(ToggleSound);
        }
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        foreach (AudioSource audioSource in allButtonSounds)
        {
            audioSource.mute = !isSoundOn;
        }

        // Change button icon (if using sprites for sound on/off)
        if (buttonImage != null)
        {
            buttonImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        }
    }
}
