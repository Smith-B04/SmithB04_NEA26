//Created: Sprint 2
//Last Edited: Sprint 3
//Purpose: Control the Health of an enemy

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class IceHealth : MonoBehaviour
{
    private CapsuleCollider2D enemyCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private float maxHealth = 100;
    private float health;
    private bool delete;
    private bool dead;
    private float deleteTimer;
    private Dictionary<string, float> damageResistances = new Dictionary<string, float>
    {
        ["physical"] = 0.75f,
        ["fire"] = 2f,
        ["magic"] = 1f,
        ["lightning"] = 1.5f,
        ["holy"] = 1f,
        ["frost"] = 0.1f,
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
        if (delete && dead)
        {
            deleteTimer -= Time.deltaTime;
        }
        if (deleteTimer < 0 && dead)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage, string damageType)
    {
        health -= (float)(damage * damageResistances[damageType]);
        animator.SetTrigger("Hurt");
        this.GetComponent<IceActions>().busy = true;
        if (health <= 0f)
        {
            animator.SetBool("Dead", true);
            enemyCollider.size = new Vector2(0.1f, 0.1f);
            if (dead == false)
            {
                int newScore = PlayerPrefs.GetInt("Score") + 100;
                PlayerPrefs.SetInt("Score", newScore);
            }
            dead = true;
            delete = true;
        }
        StartCoroutine(HurtWait());
    }

    private IEnumerator HurtWait()
    {
        yield return new WaitForSeconds(1.5f);
        this.GetComponent<IceActions>().busy = false;
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
