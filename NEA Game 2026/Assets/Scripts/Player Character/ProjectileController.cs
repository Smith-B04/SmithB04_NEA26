using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float projectileSpeed;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = 100 * this.transform.localScale.x/Mathf.Abs(this.transform.localScale.x);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Debug.Log("Hit enemy");
            enemy.TakeDamage(20, "physical");
        }

        Destroy(this.gameObject);
    }
}
