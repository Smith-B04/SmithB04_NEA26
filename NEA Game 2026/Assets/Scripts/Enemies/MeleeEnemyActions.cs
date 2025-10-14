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
        if (Math.Abs(this.GetComponent<MeleeEnemyMovement>().target.transform.position.x - this.transform.position.x) < 2.25f && this.GetComponent<MeleeEnemyMovement>().active && !animator.GetBool("Dead") && !busy)
        {
            StartCoroutine("MeleeAttack");
        }
    }

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
            //this.transform.position = new UnityEngine.Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            StartCoroutine(MeleeAttackMiddle());
        }
    }

    //Return variables to original values after set amount of time
    private IEnumerator MeleeAttackMiddle()
    {
        yield return new WaitForSeconds(0.4f);
        attackCollider.enabled = true; //Enable sword attack collider
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
            CharacterHealth player = other.GetComponent<CharacterHealth>();
            if (player != null && !enemiesHit.Contains(other.gameObject))
            {
                player.TakeDamage(40, "physical");
                enemiesHit.Add(other.gameObject);
            }
        }
    }
}
