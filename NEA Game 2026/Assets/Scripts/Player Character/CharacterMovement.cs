//Created: Sprint 2
//Last Edited: Sprint 3
//Purpose: Control movement of player character object

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public Collider2D footCollider;
    private Animator animator;
    private Rigidbody2D rb;
    public bool sprinting;
    public bool canMove;
    public bool isGrounded;
    public float speedModifier = 0.1f;
    public float jumpModifier = 350f;
    private UnityEngine.Vector2 moveInput;
    private UnityEngine.Vector2 mousePos;
    public float velocityCap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocityCap = 5f;
        canMove = true; //Player can move at start of the game
        sprinting = false; //Starts sprinting as false
        rb = this.GetComponent<Rigidbody2D>(); //Get the players rigidbody and animator
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;

        //Find the position of the mouse on the screen convert it to a world position and if past the midpoint flip character scale to change the direction its facing
        if (Camera.main.ScreenToWorldPoint(mousePos).x < this.transform.localPosition.x)
        {
            this.transform.localScale = new UnityEngine.Vector3(-1 * Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new UnityEngine.Vector3(Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }

        //Find if the character can move and set a maximum velocity they can have dependent on whether they're sprinting or not
        if (Math.Abs(rb.linearVelocity.x) < (sprinting ? 1.5 * velocityCap : velocityCap) && canMove)
        {
            animator.SetBool("isWalking", true); //Animate walking
            //Adding different amounts of force based on whether the character is sprinting or in the air
            rb.AddForce(new UnityEngine.Vector3(
                (isGrounded) ? (
                (sprinting && this.GetComponent<CharacterStamina>().stamina > 0) ?
                ((float)(1.5 * moveInput.x * speedModifier * 30000 * Time.deltaTime)) :
                (moveInput.x * speedModifier * 30000 * Time.deltaTime)
                ) :
                (((float)(0.1 * moveInput.x * speedModifier * 30000 * Time.deltaTime))),
                0,
                0
                ));
        }

        //Stop velocity when swapping directions
        if ((!(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D))))
        {
            rb.linearVelocityX = 0;
            animator.SetBool("isWalking", false);
        }
        else if ((Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.D)))
        {
            rb.linearVelocityX = 0;
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

    //Take movement input from InputSystem as a Vector2
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<UnityEngine.Vector2>();
    }

    //Take a boolean value if the key is held down
    private void OnSprint(InputValue value)
    {
        sprinting = value.isPressed;
        animator.SetBool("isSprinting", sprinting);
    }

    //Apply force upwards if jump key is inputted and player has remaining jumps
    private void OnJump()
    {
        if (!this.GetComponent<CharacterActions>().busy && isGrounded)
        {
            rb.AddForce(new UnityEngine.Vector3(0, jumpModifier, 0));
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
