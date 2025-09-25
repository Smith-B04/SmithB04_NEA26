//Created: Sprint 2
//Last Edited: Sprint 2
//Purpose: Control extra actions of player character object

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterActions : MonoBehaviour
{
    public bool busy;
    private Animator animator;
    public Collider2D attackCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.GetComponent<Animator>(); //Get the animator
        attackCollider.enabled = false; //Disable the hitbox made by sword
    }

    // Update is called once per frame
    void Update()
    {
        //Test Keys for taking damage
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.GetComponent<CharacterHealth>().TakeDamage(10, "physical");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            this.GetComponent<CharacterHealth>().TakeDamage(10, "fire");
        }
    }

    private void OnDodge()
    {
        if (this.GetComponent<CharacterStamina>().stamina > 0 && !busy && this.GetComponent<CharacterMovement>().isGrounded)
        {
            busy = true;
            this.GetComponent<CharacterStamina>().stamina -= 10; //Edit stamina accordingly
            this.GetComponent<CharacterStamina>().loadBar();
            animator.SetTrigger("Roll");
            this.GetComponent<CharacterStamina>().staminaTimer = 1.2f;
            this.GetComponent<CharacterHealth>().invincible = true;
            this.GetComponent<CharacterMovement>().sprinting = true;
            StartCoroutine(DodgeFinish());
        }
    }

    //Return variables to original values after set amount of time
    private IEnumerator DodgeFinish()
    {
        yield return new WaitForSeconds(0.6f);
        busy = false;
        this.GetComponent<CharacterHealth>().invincible = false;
        this.GetComponent<CharacterMovement>().sprinting = false;
    }

    private void OnLeftLightAttack()
    {
        if (this.GetComponent<CharacterStamina>().stamina > 0 && !busy)
        {
            busy = true;
            animator.SetTrigger("LeftLightAttack");
            Debug.Log("LeftLightAttack");
            attackCollider.enabled = true; //Enable sword attack collider
            this.GetComponent<CharacterStamina>().stamina -= 30; //Edit stamina accordingly
            this.GetComponent<CharacterStamina>().staminaTimer = 1;
            this.GetComponent<CharacterStamina>().loadBar();
            StartCoroutine(leftLightAttackFinish());
        }
    }

    //Return variables to original values after set amount of time
    private IEnumerator leftLightAttackFinish()
    {
        yield return new WaitForSeconds(0.4f);
        busy = false;
        attackCollider.enabled = false;
    }

    //Detect if sword hit anything and apply damage to it if so
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attackCollider.IsTouching(other))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(10, "physical");
            }
        }
    }
}
