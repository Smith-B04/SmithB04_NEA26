//Created: Sprint 2
//Last Edited: Sprint 2
//Purpose: Control the Health of an enemy
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private CapsuleCollider2D enemyCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    public float maxHealth;
    public float health;
    private bool dead;
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
        dead = false;
        enemyCollider = this.GetComponent<CapsuleCollider2D>(); //Get enemy collider, rigid body, animator and sprite renderer
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
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
            animator.SetBool("Dead", true);
            enemyCollider.size = new Vector2(0.1f,0.1f);
            dead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (dead)
            {
                rb.simulated = false;
                sr.sortingOrder = -1;
            }
        }
    }
}
