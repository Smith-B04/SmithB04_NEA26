//Created: Sprint 3
//Last Edited: Sprint 3
//Purpose: Control the actions of an enemy

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeEnemyActions : MonoBehaviour
{
    private Rigidbody2D rb;
    List<GameObject> enemiesHit = new List<GameObject>();
    public bool busy;
    private Animator animator;
    public Collider2D attackCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); //Get the rigid body
        animator = this.GetComponent<Animator>(); //Get the animator
        attackCollider.enabled = false; //Disable the hitbox made by Axe
    }

    // Update is called once per frame
    void Update()
    {
        // See if the player is in range to attack them
        if (Math.Abs(this.GetComponent<MeleeEnemyMovement>().target.transform.position.x - this.transform.position.x) < 2f && this.GetComponent<MeleeEnemyMovement>().active && !animator.GetBool("Dead") && !busy && Math.Abs(this.GetComponent<MeleeEnemyMovement>().target.transform.position.y - this.transform.position.y) < 3f)
        {
            StartCoroutine("MeleeAttack");
        }
    }

    // Jump and perform an attack
    private void MeleeAttack()
    {
        if (!busy)
        {
            Debug.Log("Orc Attack");
            busy = true;
            animator.SetTrigger("MeleeAttack");
            if (this.GetComponent<MeleeEnemyMovement>().isGrounded)
            {
                rb.AddForce(Vector2.up * 250);
            }
            StartCoroutine(MeleeAttackMiddle());
        }
    }

    // Activates the attack hit box at the same time as the animation would indicate damage
    private IEnumerator MeleeAttackMiddle()
    {
        yield return new WaitForSeconds(0.4f);
        attackCollider.enabled = true; // Enable sword attack collider
        StartCoroutine(MeleeAttackFinish());
    }

    // Return variables to original values after set amount of time
    private IEnumerator MeleeAttackFinish()
    {
        yield return new WaitForSeconds(0.2f);
        enemiesHit.Clear();
        attackCollider.enabled = false;
        StartCoroutine(attackWait());
    }

    // Pause the enemy temporarily so they don't attack non-stop
    private IEnumerator attackWait()
    {
        yield return new WaitForSeconds((float)(UnityEngine.Random.value*0.5) + 0.5f);
        busy = false;
    }

    //Detect if sword hit anything and apply damage to it if so
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attackCollider.IsTouching(other))
        {
            // Search for the player health script to use its take damage function
            CharacterHealth player = other.GetComponent<CharacterHealth>();
            if (player != null && !enemiesHit.Contains(other.gameObject))
            {
                player.TakeDamage(15, "physical");
                enemiesHit.Add(other.gameObject);
            }
        }
    }
}
