//Created: Sprint 7
//Last Edited: Sprint 7
//Purpose: Control the actions of an enemy

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceActions : MonoBehaviour
{
    public GameObject projectile;
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
        // Check if in melee range of the player
        if (Math.Abs(this.GetComponent<IceMovement>().target.transform.position.x - this.transform.position.x) < 10f && this.GetComponent<IceMovement>().active && !animator.GetBool("Dead") && !busy && Math.Abs(this.GetComponent<IceMovement>().target.transform.position.y - this.transform.position.y) < 5.5f)
        {
            StartCoroutine("MeleeAttack");
        }
    }

    // Start the attack animation
    private void MeleeAttack()
    {
        if (!busy)
        {
            busy = true;
            animator.SetTrigger("MeleeAttack");
            StartCoroutine(MeleeAttackMiddle());
        }
    }

    // Activate attack hitbox in time with the animation
    private IEnumerator MeleeAttackMiddle()
    {
        yield return new WaitForSeconds(0.55f);
        attackCollider.enabled = true; //Enable attack collider
        StartCoroutine(MeleeAttackFinish());
    }

    //Return variables to original values after set amount of time
    private IEnumerator MeleeAttackFinish()
    {
        yield return new WaitForSeconds(0.2f);
        enemiesHit.Clear();
        attackCollider.enabled = false;
        StartCoroutine(attackWait());
    }

    // Gives a random amount of time between attacks
    private IEnumerator attackWait()
    {
        yield return new WaitForSeconds((float)(UnityEngine.Random.value * 0.5) + 2f);
        busy = false;
    }

    // Start the throw animation 
    private void RangedAttack()
    {
        if (!busy)
        {
            busy = true;
            animator.SetTrigger("ThrowAttack");
            StartCoroutine(RangedAttackFinish());
        }
    }
    
    // Instantiate a projectile facing the same direction
    private IEnumerator RangedAttackFinish()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject newProjectile = Instantiate(
                projectile,
                new Vector2(
                    this.transform.position.x + 2.15f * this.transform.localScale.x / Math.Abs(this.transform.localScale.x),
                    this.transform.position.y),
                Quaternion.identity); //Instantiate projectile
        newProjectile.transform.localScale = new Vector3(
            this.transform.localScale.x / Math.Abs(this.transform.localScale.x) * newProjectile.transform.localScale.x,
            newProjectile.transform.localScale.y,
            newProjectile.transform.localScale.z
            ); //Change arrows starting direction to the same as the enemy
        StartCoroutine(attackWait());
    }

    //Detect if melee hit character and apply damage to it if so
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attackCollider.IsTouching(other))
        {
            CharacterHealth player = other.GetComponent<CharacterHealth>();
            if (player != null && !enemiesHit.Contains(other.gameObject))
            {
                player.TakeDamage(30, "physical");
                enemiesHit.Add(other.gameObject);
            }
        }
    }
}
