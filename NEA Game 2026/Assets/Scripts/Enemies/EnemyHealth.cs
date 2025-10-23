//Created: Sprint 2
//Last Edited: Sprint 2
//Purpose: Control the Health of an enemy
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private CapsuleCollider2D enemyCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    public float maxHealth;
    public float health;
    private bool delete;
    private bool dead;
    private float deleteTimer;
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
        deleteTimer = 1;
        delete = false;
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
        if (delete && dead) {
            deleteTimer -= Time.deltaTime;
        }
        if (deleteTimer < 0 && dead)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage, string damageType)
    {
        health -= (float)(damage * damageResistances[damageType]);
        animator.SetTrigger("Hurt");
        if (this.GetComponent<RangedEnemyActions>() != null) 
        {
            this.GetComponent<RangedEnemyActions>().busy = true; 
        }
        else 
        { 
            this.GetComponent<MeleeEnemyActions>().busy = true; 
        }
        if (health <= 0f)
        {
            animator.SetBool("Dead", true);
            enemyCollider.size = new Vector2(0.1f,0.1f);
            dead = true;
            delete = true;
        }
        StartCoroutine(HurtWait());
    }

    private IEnumerator HurtWait()
    {
        yield return new WaitForSeconds(1.5f);
        if (this.GetComponent<RangedEnemyActions>() != null)
        {
            this.GetComponent<RangedEnemyActions>().busy = false;
        }
        else
        {
            this.GetComponent<MeleeEnemyActions>().busy = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (dead)
            {
                delete = false;
                rb.simulated = false;
                sr.sortingOrder = -1;
            }
        }
    }
}
