using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class MovementScript : MonoBehaviour {

    public UnityEngine.UI.Image healthBar;
    private Rigidbody2D rb;
    public float speedModifier;
	public bool sprinting;
	private bool isGrounded;
    public float jumpModifier;
    public float maxHealth;
    public float health;
    public Collider2D attackCollider;

    Dictionary<string, float> damageResistances = new Dictionary<string, float> 
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
    void Start () {
        health = maxHealth;
		rb = this.GetComponent<Rigidbody2D> ();
        sprinting = false;
        attackCollider.enabled = false;
    }

	// Update is called once per frame
	void Update() {
        if (Input.GetKey(KeyCode.P))
        {
            TakeDamage(1, "physical");
        }

        if (Input.mousePosition.x < Screen.width/2)
        {
            this.transform.localScale = new Vector3(-1 * Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        } else {
            this.transform.localScale = new Vector3(Math.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
    }


        void FixedUpdate () {
		if (Input.GetKey (KeyCode.A)) {
            /*
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
            */
            if (rb.linearVelocity.x > (Input.GetKey(KeyCode.LeftShift) ? -7.5 : -5))
            {
                rb.AddForce(new Vector3(
                    (isGrounded) ? (
                    Input.GetKey(KeyCode.LeftShift) ?
                    ((float)(1.5 * -speedModifier * 300)) :
                    (-speedModifier * 300)
                    ) :
                    (((float)(0.1 * -speedModifier * 300))),
                    0,
                    0
                    ));
            }
        }
		if (Input.GetKey (KeyCode.D)) {
            /*
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
            */
            if (rb.linearVelocity.x < (Input.GetKey(KeyCode.LeftShift) ? 7.5 : 5)) 
            { 
                rb.AddForce(new Vector3(
                    (isGrounded) ? (
                    Input.GetKey(KeyCode.LeftShift) ?
                    ((float)(1.5 * speedModifier * 300)) :
                    (speedModifier * 300)
                    ) :
                    (((float)(0.1 * speedModifier * 300))),
                    0,
                    0
                    ));
            }
        }

        if (!(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D)))
        {
            rb.linearVelocityX = 0;
            //rb.linearVelocityX += (rb.linearVelocity.x == 0) ? (0) : ((rb.linearVelocity.x < 0) ? (1) : (-1)) ;
        }
    }

    public void TakeDamage(int damage, string /*change to set type*/ damageType)
    {
        health -= (float)(damage * damageResistances[damageType]);
        loadBars();
    }

    public void loadBars()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    private void OnJump()
	{
        rb.AddForce(new Vector3(0, jumpModifier, 0));
    }

    private void OnLeftLightAttack()
    {
        Debug.Log("LeftLightAttack");
        attackCollider.enabled = true;
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