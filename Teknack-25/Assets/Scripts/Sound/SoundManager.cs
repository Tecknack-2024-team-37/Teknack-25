// using UnityEngine;
// using UnityEngine.UI;

// public class SoundManager : MonoBehaviour
// {
//     private AudioSource[] allButtonSounds;
//     private bool isSoundOn = true;
//     public Button soundToggleButton;
//     public Sprite soundOnSprite;
//     public Sprite soundOffSprite;
//     private Image buttonImage;

//     private void Start()
//     {
//         // Find all AudioSources in the scene (attached to buttons)
//         allButtonSounds = FindObjectsOfType<AudioSource>();

//         if (soundToggleButton != null)
//         {
//             buttonImage = soundToggleButton.GetComponent<Image>();
//             soundToggleButton.onClick.AddListener(ToggleSound);
//         }
//     }

//     public void ToggleSound()
//     {
//         isSoundOn = !isSoundOn;

//         foreach (AudioSource audioSource in allButtonSounds)
//         {
//             audioSource.mute = !isSoundOn;
//         }

//         // Change button icon (if using sprites for sound on/off)
//         if (buttonImage != null)
//         {
//             buttonImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
//         }
//     }
// }
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] allSounds; // To hold all AudioSources
    private bool isSoundOn;
    public Button soundToggleButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    private Image buttonImage;

    private void Start()
    {
        // Get all AudioSources in the scene
        FetchAllAudioSources();

        // Get button image component
        if (soundToggleButton != null)
        {
            buttonImage = soundToggleButton.GetComponent<Image>();
            soundToggleButton.onClick.AddListener(ToggleSound);
        }

        // Load previous sound setting (default is sound ON)
        isSoundOn = PlayerPrefs.GetInt("SoundSetting", 1) == 1;

        // Apply initial sound state
        ApplySoundState();
    }

    private void FetchAllAudioSources()
    {
        allSounds = FindObjectsOfType<AudioSource>(); // Refresh AudioSources
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("SoundSetting", isSoundOn ? 1 : 0); // Save state
        PlayerPrefs.Save();

        ApplySoundState();
    }

    private void ApplySoundState()
    {
        FetchAllAudioSources(); // ✅ Ensure we get newly created AudioSources

        // Mute/unmute all sounds
        foreach (AudioSource audioSource in allSounds)
        {
            audioSource.mute = !isSoundOn;
        }

        // Mute AudioListener as a fallback
        AudioListener.pause = !isSoundOn;

        // Change button icon
        if (buttonImage != null)
        {
            buttonImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        }
    }
}