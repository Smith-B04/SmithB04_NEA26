//Created: Sprint 3
//Last Edited: Sprint 3
//Purpose: Control the actions of an enemy

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyActions : MonoBehaviour
{
    public GameObject arrow;
    public bool busy;
    private Animator animator;
    public Collider2D attackCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.GetComponent<Animator>(); //Get the animator
        attackCollider.enabled = false; //Disable the hitbox made by Axe
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Math.Abs(this.GetComponent<EnemyMovement>().target.transform.position.x - this.transform.position.x) < 2.25f && this.GetComponent<EnemyMovement>().active && !animator.GetBool("Dead"))
        {
            StartCoroutine("LeftLightAttack");
        }
    }

    private void LeftLightAttack()
    {
        if (!busy)
        {
            Debug.Log("Orc Attack");
            busy = true;
            animator.SetTrigger("Attack");
            this.transform.position = new UnityEngine.Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            attackCollider.enabled = true; //Enable sword attack collider
            StartCoroutine(leftLightAttackFinish());
        }
    }

    //Return variables to original values after set amount of time
    private IEnumerator leftLightAttackFinish()
    {
        yield return new WaitForSeconds(1f);
        busy = false;
        attackCollider.enabled = false;
    }

    //Detect if sword hit anything and apply damage to it if so
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attackCollider.IsTouching(other))
        {
            CharacterHealth player = other.GetComponent<CharacterHealth>();
            if (player != null)
            {
                player.TakeDamage(40, "physical");
            }
        }
    }
}
