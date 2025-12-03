//Created: Sprint 2
//Last Edited: Sprint 3
//Purpose: Control any projectile used by any character.

using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public string type;
    public float projectileSpeed; //Can be changed for different projectiles the script is attached to
    private Rigidbody2D rb;
    private float timer;
    private float rotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 5f;
        rb = this.GetComponent<Rigidbody2D>(); //Get the players rigidbody
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotation += Time.deltaTime * 75;

        if (type == "Axe")
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, rotation);
        }
        rb.linearVelocityX = projectileSpeed * this.transform.localScale.x/Mathf.Abs(this.transform.localScale.x); //Move the projectile in the direction it is facing
        timer -= Time.deltaTime;

        if (timer < 0) 
        {
            Destroy(this.gameObject);
        }
    }

    //Deal damage to any enemy hit by the arrow 
    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
        CharacterHealth player = other.gameObject.GetComponent<CharacterHealth>();
        if (enemy != null)
        {
            Debug.Log("Hit enemy");
            enemy.TakeDamage(10, "physical");
        }
        if (player != null)
        {
            Debug.Log("Hit player");
            player.TakeDamage(10, "physical");
        }

        Destroy(this.gameObject); //Destroy arrow in collision
    }
}
