//Created: Sprint 3
//Last Edited: Sprint 3
//Purpose: Control Sites of Grace.

using UnityEngine;

public class GraceController : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterHealth player = other.gameObject.GetComponent<CharacterHealth>();
        if (player != null)
        {
            player.health = player.maxHealth;
            player.gameObject.GetComponent<CharacterActions>().flasksRemaining = player.gameObject.GetComponent<CharacterActions>().maxFlasks;
            player.invincible = true;
            player.loadBar();
            audioSource.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CharacterHealth>() != null)
        {
            //potentially add ability to improve all stats here in a menu or see current progress
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CharacterHealth player = other.gameObject.GetComponent<CharacterHealth>();
        if (player != null)
        {
            player.invincible = false;
        }
    }
}
