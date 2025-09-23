using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //private Collider2D collider;
    private Rigidbody2D rb;
    private Animator animator;
    public float maxHealth;
    private float health;
    private Dictionary<string, float> damageResistances = new Dictionary<string, float>
    {
        ["physical"] = 0.5f,
        ["fire"] = 1f,
        ["magic"] = 1f,
        ["lightning"] = 1f,
        ["holy"] = 1f,
        ["frost"] = 1f,
        ["poison"] = 1f,
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //collider = this.GetComponent<Collider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage, string damageType)
    {
        health -= (float)(damage * damageResistances[damageType]);
        animator.SetTrigger("Hurt");
        if (health <= 0f)
        {
            rb.simulated = false;
            animator.SetBool("Dead", true);
        }
    }
}
