//Created: Sprint 2
//Last Edited: Sprint 3
//Purpose: Control extra actions of player character object

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterActions : MonoBehaviour
{
    List<GameObject> enemiesHit = new List<GameObject>();
    private SpriteRenderer spriteRenderer;
    public int maxFlasks;
    public int flasksRemaining;
    public GameObject arrow;
    public bool busy;
    private Animator animator;
    public Collider2D attackCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flasksRemaining = maxFlasks;
        animator = this.GetComponent<Animator>(); //Get the animator
        spriteRenderer = this.GetComponent<SpriteRenderer>();
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

    private void OnFlask()
    {
        if (flasksRemaining > 0 && !busy)
        {
            flasksRemaining -= 1;
            busy = true;
            spriteRenderer.color = Color.lightGreen;
            this.GetComponent<CharacterHealth>().health = (this.GetComponent<CharacterHealth>().health + 50 < this.GetComponent<CharacterHealth>().maxHealth) ? this.GetComponent<CharacterHealth>().health + 50 : this.GetComponent<CharacterHealth>().maxHealth;
            this.GetComponent<CharacterHealth>().loadBar();
            StartCoroutine(FlaskFinish());
        }
    }

    private IEnumerator FlaskFinish()
    {
        yield return new WaitForSeconds(0.5f);
        busy = false;
        spriteRenderer.color = Color.white;
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
        yield return new WaitForSeconds(0.3f);
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
            this.GetComponent<CharacterStamina>().stamina -= 30; //Edit stamina accordingly
            this.GetComponent<CharacterStamina>().staminaTimer = 1;
            this.GetComponent<CharacterStamina>().loadBar();
            StartCoroutine(leftLightAttackMiddle());
        }
    }

    //Return variables to original values after set amount of time
    private IEnumerator leftLightAttackMiddle()
    {
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = true; //Enable sword attack collider
        StartCoroutine(leftLightAttackFinish());
    }

    //Return variables to original values after set amount of time
    private IEnumerator leftLightAttackFinish()
    {
        yield return new WaitForSeconds(0.2f);
        enemiesHit.Clear();
        busy = false;
        attackCollider.enabled = false;
    }

    private void OnRightLightAttack()
    {
        if (this.GetComponent<CharacterStamina>().stamina > 0 && !busy)
        {
            this.GetComponent<CharacterMovement>().canMove = false;
            busy = true;
            animator.SetTrigger("RightLightAttack");
            Debug.Log("RightLightAttack");
            this.GetComponent<CharacterStamina>().stamina -= 30; //Edit stamina accordingly
            this.GetComponent<CharacterStamina>().staminaTimer = 1;
            this.GetComponent<CharacterStamina>().loadBar();
            StartCoroutine(rightLightAttackFinish());
        }
    }

    //Return variables to original values after set amount of time
    private IEnumerator rightLightAttackFinish()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject newArrow = Instantiate(
                arrow,
                new Vector2(
                    this.transform.position.x + 1.75f * Math.Sign(this.transform.localScale.x),
                    this.transform.position.y),
                Quaternion.identity); //Instantiate arrow
        newArrow.transform.localScale = new Vector3(
            Math.Sign(this.transform.localScale.x) * newArrow.transform.localScale.x,
            newArrow.transform.localScale.y, 
            newArrow.transform.localScale.z
            ); //Change arrows starting direction to the same as the player
        this.GetComponent<CharacterMovement>().canMove = true;
        busy = false;
    }


    //Detect if sword hit anything and apply damage to it if so
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attackCollider.IsTouching(other))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null && !enemiesHit.Contains(other.gameObject))
            {
                enemy.TakeDamage(10, "physical");
                enemiesHit.Add(other.gameObject);
            }
        }
    }
}
