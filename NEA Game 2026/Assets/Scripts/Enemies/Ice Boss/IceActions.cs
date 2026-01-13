//Created: Sprint 3
//Last Edited: Sprint 3
//Purpose: Control the actions of an enemy

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;
using static UnityEngine.GraphicsBuffer;

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
        if (Math.Abs(this.GetComponent<IceMovement>().target.transform.position.x - this.transform.position.x) < 10f && this.GetComponent<IceMovement>().active && !animator.GetBool("Dead") && !busy && Math.Abs(this.GetComponent<IceMovement>().target.transform.position.y - this.transform.position.y) < 5f)
        {
            StartCoroutine("MeleeAttack");
        }
    }

    private void MeleeAttack()
    {
        if (!busy)
        {
            busy = true;
            animator.SetTrigger("MeleeAttack");
            StartCoroutine(MeleeAttackMiddle());
        }
    }

    //Return variables to original values after set amount of time
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

    private IEnumerator attackWait()
    {
        yield return new WaitForSeconds((float)(UnityEngine.Random.value * 0.5) + 2f);
        busy = false;
    }

    private void RangedAttack()
    {
        if (!busy)
        {
            busy = true;
            animator.SetTrigger("ThrowAttack");
            StartCoroutine(RangedAttackFinish());
        }
    }

    private IEnumerator RangedAttackFinish()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject newAxe = Instantiate(
                projectile,
                new Vector2(
                    this.transform.position.x + 2.15f * this.transform.localScale.x / Math.Abs(this.transform.localScale.x),
                    this.transform.position.y),
                Quaternion.identity); //Instantiate arrow
        newAxe.transform.localScale = new Vector3(
            this.transform.localScale.x / Math.Abs(this.transform.localScale.x) * newAxe.transform.localScale.x,
            newAxe.transform.localScale.y,
            newAxe.transform.localScale.z
            ); //Change arrows starting direction to the same as the enemy
        StartCoroutine(attackWait());
    }

    //Detect if sword hit anything and apply damage to it if so
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
