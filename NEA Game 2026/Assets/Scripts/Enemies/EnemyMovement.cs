//Created: Sprint 3
//Last Edited: Sprint 3
//Purpose: Control the movement of an enemy

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject enemyPivot;
    public Collider2D footCollider;
    private Animator animator;
    public GameObject target;
    private Rigidbody2D rb;
    public bool isGrounded;
    private float speedModifier = 0.1f;
    private float jumpModifier = 300f;
    public bool active;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        active = false;
        rb = this.GetComponent<Rigidbody2D>(); //Get the players rigidbody and animator
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active && Math.Abs(rb.linearVelocityX) < 5 && !animator.GetBool("Dead"))
        {
            if (target.transform.position.x < enemyPivot.transform.position.x)
            {
                enemyPivot.transform.localScale = new UnityEngine.Vector3(-1 * Math.Abs(enemyPivot.transform.localScale.x), enemyPivot.transform.localScale.y, enemyPivot.transform.localScale.z);
            }
            else
            {
                enemyPivot.transform.localScale = new UnityEngine.Vector3(Math.Abs(enemyPivot.transform.localScale.x), enemyPivot.transform.localScale.y, enemyPivot.transform.localScale.z);
            }

            if (Math.Abs(target.transform.position.x - enemyPivot.transform.position.x) > 2.25f)
            {
                rb.AddForce(new UnityEngine.Vector3(
                       enemyPivot.transform.localScale.x / Math.Abs(enemyPivot.transform.localScale.x) * speedModifier * 30000 * Time.deltaTime, 0, 0));
            }
            else
            {
                rb.linearVelocityX = 0;
            }
        }
        else
        {
            rb.linearVelocityX = 0;
        }

        RaycastHit2D gapHit = Physics2D.Raycast(footCollider.transform.position, new UnityEngine.Vector2(1.4f, -1.4f), 0.1f);
        Debug.DrawRay(footCollider.transform.position, new UnityEngine.Vector2(1.4f, -1.4f), Color.green, 5f);
        if (!gapHit) //raycast from feet forwards for the jump over obstical and raycast from feet diagonally down 45 degrees to jump over gap.
        {
            if (isGrounded)
            {
                rb.AddForce(new UnityEngine.Vector3(0, jumpModifier, 0));
            }
        }
    }

    //Set grounded variable and reset jumps
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (footCollider.IsTouching(other))
            {
                isGrounded = true;
            }
        }
    }

    //Set grounded variable
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (!footCollider.IsTouching(other))
            {
                isGrounded = false;
            }
        }
    }
}
