//Created: Sprint 3
//Last Edited: Sprint 3
//Purpose: Control the movement of an enemy

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class IceMovement : MonoBehaviour
{
    public Collider2D footCollider;
    private Animator animator;
    public GameObject target;
    private Rigidbody2D rb;
    public bool isGrounded;
    public float speedModifier = 5f;
    public bool active;
    private int terrainMask;
    private int flippedDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        terrainMask = LayerMask.GetMask("Ground");
        active = true;
        rb = this.GetComponent<Rigidbody2D>(); //Get the players rigidbody and animator
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active && Math.Abs(rb.linearVelocityX) < 3 && !animator.GetBool("Dead"))
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
                this.transform.localScale = new UnityEngine.Vector3(1 * Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                if (flippedDirection != 1)
                {
                    this.transform.position = this.transform.position + new UnityEngine.Vector3(1f, 0, 0);
                    flippedDirection = 1;
                }
            }

            if (Math.Abs(target.transform.position.x - this.transform.position.x) > 10f)
            {
                rb.AddForce(new UnityEngine.Vector3(
                       Math.Sign(this.transform.localScale.x) * speedModifier * 30000 * Time.deltaTime, 0, 0));
                animator.SetBool("Walking", true);
            }
            else
            {
                rb.linearVelocityX = 0;
                animator.SetBool("Walking", false);
            }
        }
        else
        {
            //rb.linearVelocityX = 0;
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
