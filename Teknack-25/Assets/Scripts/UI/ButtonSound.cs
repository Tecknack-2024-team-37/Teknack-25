using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource clickSound;

    public void PlaySound()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }
}
