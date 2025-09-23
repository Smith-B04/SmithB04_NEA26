//Created: Sprint 2
//Last Edited: Sprint 2
//Purpose: Control movement of player character object

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public bool sprinting;
    private bool isGrounded;
    private float speedModifier = 0.1f;
    private float jumpModifier = 300f;
    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprinting = false;
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x < Screen.width / 2)
        {
            this.transform.localScale = new Vector3(-1 * Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }

        if (Math.Abs(rb.linearVelocity.x) < (sprinting ? 7.5 : 5) && !this.GetComponent<CharacterActions>().busy)
        {
            animator.SetBool("isWalking", true);
            rb.AddForce(new Vector3(
                (isGrounded) ? (
                (sprinting && this.GetComponent<CharacterStamina>().stamina > 0 && !this.GetComponent<CharacterActions>().busy) ?
                ((float)(1.5 * moveInput.x * speedModifier * 30000 * Time.deltaTime)) :
                (moveInput.x * speedModifier * 30000 * Time.deltaTime)
                ) :
                (((float)(0.1 * moveInput.x * speedModifier * 30000 * Time.deltaTime))),
                0,
                0
                ));
        }

        if ((!(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D))) || ((Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.D))))
        {
            rb.linearVelocityX = 0;
            animator.SetBool("isWalking", false);
            sprinting = false;
            animator.SetBool("isSprinting", false);
        }
        else if ((Input.GetKey(KeyCode.D)) && (rb.linearVelocity.x < 0))
        {
            rb.linearVelocityX = 0;
        }
        else if ((Input.GetKey(KeyCode.A)) && (rb.linearVelocity.x > 0))
        {
            rb.linearVelocityX = 0;
        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnSprint()
    {
        sprinting = !sprinting;
        animator.SetBool("isSprinting", sprinting);
    }

    private void OnJump()
    {
        rb.AddForce(new Vector3(0, jumpModifier, 0));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
