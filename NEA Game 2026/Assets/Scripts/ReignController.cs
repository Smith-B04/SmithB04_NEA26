//Created: Sprint 5
//Last Edited: Sprint 5
//Purpose: Control the Reign...

using UnityEngine;

public class ReignController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CharacterHealth player = other.GetComponent<CharacterHealth>();
        if (player != null)
        {
            player.TakeDamage(5*Time.deltaTime, "fire");
            player.TakeDamage(5*Time.deltaTime, "magic");
        }
    }
}
