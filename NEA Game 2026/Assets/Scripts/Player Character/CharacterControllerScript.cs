//Created: Sprint 1
//Last Edited: Sprint 1
//Purpose: Control bahaviours of player character object

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterControllerScript : MonoBehaviour
{
    private Animator animator;
    public UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Image staminaBar;
    public Collider2D attackCollider;
    private Rigidbody2D rb;
    private bool sprinting;
    private bool isGrounded;
    private float speedModifier = 0.1f;
    private float jumpModifier = 300f;
    public float maxHealth;
    private float health;
    public float maxStamina;
    private float stamina;
    private float staminaTimer;
    private Vector2 moveInput;
    private bool invincible;
    private bool busy;
    private Dictionary<string, float> damageResistances = new Dictionary<string, float>
    {
        ["physical"] = 0.5f,
        ["fire"] = 1f,
        ["magic"] = 1f,
        ["lightning"] = 1f,
        ["holy"] = 1f,
        ["frost"] = 1f,
        ["poison"] = 1f,
    };

    // Use this for initialization
    void Start()
    {
        busy = false;
        invincible = false;
        health = maxHealth;
        stamina = maxStamina;
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        sprinting = false;
        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10, "physical");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(10, "fire");
        }

        if (Input.mousePosition.x < Screen.width / 2)
        {
            this.transform.localScale = new Vector3(-1 * Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }

        if (staminaTimer < 0)
        {
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime * 100;
                loadBars();
            }
            else
            {
                stamina = maxStamina;
                loadBars();
            }
        }
        else
        {
            staminaTimer -= Time.deltaTime;
        }
    }


    void FixedUpdate()
    {
        /*
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = new Vector3(
                (isGrounded) ? (
                Input.GetKey(KeyCode.LeftShift) ?
                (this.transform.position.x - (float)(1.5 * speedModifier)) :
                (this.transform.position.x - speedModifier)
                ) :
                ((this.transform.position.x - (float)(0.5 * speedModifier))),
                this.transform.position.y,
                this.transform.position.z
                );
            if (rb.linearVelocity.x > (Input.GetKey(KeyCode.LeftShift) ? -7.5 : -5))
            {
                rb.AddForce(new Vector3(
                    (isGrounded) ? (
                    (Input.GetKey(KeyCode.LeftShift) && stamina > 0) ?
                    ((float)(1.5 * -speedModifier * 300)) :
                    (-speedModifier * 300)
                    ) :
                    (((float)(0.1 * -speedModifier * 300))),
                    0,
                    0
                    ));
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = new Vector3(
                (isGrounded) ? (
                Input.GetKey(KeyCode.LeftShift) ?
                (this.transform.position.x + (float)(1.5 * speedModifier)) :
                (this.transform.position.x + speedModifier)
                ) :
                ((this.transform.position.x + (float)(0.5 * speedModifier))),
                this.transform.position.y,
                this.transform.position.z
                );
            if (rb.linearVelocity.x < (Input.GetKey(KeyCode.LeftShift) ? 7.5 : 5))
            {
                rb.AddForce(new Vector3(
                    (isGrounded) ? (
                    (Input.GetKey(KeyCode.LeftShift) && stamina > 0) ?
                    ((float)(1.5 * speedModifier * 300)) :
                    (speedModifier * 300)
                    ) :
                    (((float)(0.1 * speedModifier * 300))),
                    0,
                    0
                    ));
            }
        }
        */

        if (Math.Abs(rb.linearVelocity.x) < (sprinting ? 7.5 : 5) && !busy)
        {
            animator.SetBool("isWalking", true);
            rb.AddForce(new Vector3(
                (isGrounded) ? (
                (sprinting && stamina > 0 && !busy) ?
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

    public void TakeDamage(int damage, string /*change to set type*/ damageType)
    {
        if (invincible != true)
        {
            health -= (float)(damage * damageResistances[damageType]);
            loadBars();
        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void loadBars()
    {
        healthBar.fillAmount = health / maxHealth;
        staminaBar.fillAmount = stamina / maxStamina;
    }

    private void OnSprint()
    {
        sprinting = !sprinting;
        animator.SetBool("isSprinting", sprinting);
    }

    private void OnDodge()
    {
        if (stamina > 0 && !busy)
        {
            busy = true;
            stamina -= 10;
            loadBars();
            animator.SetTrigger("Roll");
            staminaTimer = 1.2f;
            invincible = true;
            sprinting = true;
            StartCoroutine(DodgeFinish());
        }
    }

    private IEnumerator DodgeFinish()
    {
        yield return new WaitForSeconds(0.6f);
        busy = false;
        invincible = false;
        sprinting = false;
    }

    private void OnJump()
    {
        rb.AddForce(new Vector3(0, jumpModifier, 0));
    }

    private void OnLeftLightAttack()
    {
        if (stamina > 0 && !busy)
        {
            busy = true;
            animator.SetTrigger("LeftLightAttack");
            Debug.Log("LeftLightAttack");
            attackCollider.enabled = true;
            stamina -= 30;
            staminaTimer = 1;
            loadBars();
            StartCoroutine(leftLightAttackFinish());
        }
    }

    private IEnumerator leftLightAttackFinish()
    {
        yield return new WaitForSeconds(0.4f);
        busy = false;
        attackCollider.enabled = false;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(10, "physical");
        }
    }
}