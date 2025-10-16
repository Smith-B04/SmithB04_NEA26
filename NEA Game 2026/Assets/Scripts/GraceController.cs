using UnityEngine;

public class GraceController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
