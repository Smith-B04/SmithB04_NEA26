//Created: Sprint 2
//Last Edited: Sprint 2
//Purpose: Control any projectile used by any character.
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projectileSpeed; //Can be changed for different projectiles the script is attached to
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); //Get the players rigidbody
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocityX = projectileSpeed * this.transform.localScale.x/Mathf.Abs(this.transform.localScale.x); //Move the projectile in the direction it is facing
    }

    //Deal damage to any enemy hit by the arrow 
    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
        CharacterHealth player = other.gameObject.GetComponent<CharacterHealth>();
        if (enemy != null)
        {
            Debug.Log("Hit enemy");
            enemy.TakeDamage(20, "physical");
        }
        if (player != null)
        {
            Debug.Log("Hit player");
            player.TakeDamage(20, "physical");
        }

        Destroy(this.gameObject); //Destroy arrow in collision
    }
}
