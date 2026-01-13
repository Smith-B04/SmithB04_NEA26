//Created: Sprint 3
//Last Edited: Sprint 3
//Purpose: Control the movement of an enemy

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MeleeEnemyMovement : MonoBehaviour
{
    public Collider2D footCollider;
    private Animator animator;
    public GameObject target;
    private Rigidbody2D rb;
    public bool isGrounded;
    public float speedModifier = 0.1f;
    public float jumpModifier = 475f;
    public bool active;
    private int terrainMask;
    private int flippedDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        terrainMask = LayerMask.GetMask("Ground");
        active = false;
        rb = this.GetComponent<Rigidbody2D>(); //Get the players rigidbody and animator
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active && Math.Abs(rb.linearVelocityX) < 5 && !animator.GetBool("Dead"))
        {
            if (target.transform.position.x < this.transform.position.x)
            {
                this.transform.localScale = new UnityEngine.Vector3(-1 * Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                if (flippedDirection != -1)
                {
                    this.transform.position = this.transform.position + new UnityEngine.Vector3(-1f, 0, 0);
                    flippedDirection = -1;
                }
            }
            else
            {
                this.transform.localScale = new UnityEngine.Vector3(Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                if (flippedDirection != 1)
                {
                    this.transform.position = this.transform.position + new UnityEngine.Vector3(1f, 0, 0);
                    flippedDirection = 1;
                }
            }

            if (Math.Abs(target.transform.position.x - this.transform.position.x) > 2f)
            {
                rb.AddForce(new UnityEngine.Vector3(
                       Math.Sign(this.transform.localScale.x) * speedModifier * 30000 * Time.deltaTime, 0, 0));
                animator.SetBool("Walking", true);

                RaycastHit2D obstacleHit = Physics2D.Raycast(footCollider.transform.position + new UnityEngine.Vector3(0, -0.1f, 0), UnityEngine.Vector2.right * Mathf.Sign(this.gameObject.transform.localScale.x), 2f, terrainMask);
                if (obstacleHit.collider != null) //raycast from feet forwards for the jump over obstical and raycast from feet diagonally down 45 degrees to jump over gap.
                {
                    Debug.Log("Enemy jump!");
                    if (isGrounded)
                    {
                        rb.AddForce(new UnityEngine.Vector3(0, jumpModifier, 0));
                    }
                }
            }
            else
            {
                rb.linearVelocityX = 0;
                animator.SetBool("Walking", false);
            }
        }
        else
        {
            rb.linearVelocityX = 0;
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
