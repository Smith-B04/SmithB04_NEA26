using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private AudioSource audioSource;
    public AudioClip healSound;
    public AudioClip swordSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwordSound()
    {
        audioSource.PlayOneShot(swordSound,1);
    }

    public void HealSound()
    {
        audioSource.PlayOneShot(healSound, 1);
    }
}
