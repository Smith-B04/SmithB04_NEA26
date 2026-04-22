//Created: Sprint 6
//Last Edited: Sprint 6
//Purpose: Control sounds from the player.

using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip healSound;
    public AudioClip swordSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // called when the sword is used
    public void SwordSound()
    {
        audioSource.PlayOneShot(swordSound,1);
    }

    // called when heal is used
    public void HealSound()
    {
        audioSource.PlayOneShot(healSound, 1);
    }
}
