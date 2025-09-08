using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class MovementScript : MonoBehaviour {

	
	public GameObject playerCharacter;
    public UnityEngine.UI.Image healthBar;
    private Rigidbody2D rb;
    public float speedModifier;
	public bool sprinting;
	private bool isGrounded;
    public float jumpModifier;
    public int maxHealth;
    private int health;


    // Use this for initialization
    void Start () {
        health = maxHealth;
		rb = playerCharacter.GetComponent<Rigidbody2D> ();
		sprinting = false;
    }

	// Update is called once per frame
	void Update() {
        TakeDamage(5, "hello");
    }


    void FixedUpdate () {
		if (Input.GetKey (KeyCode.A)) {
            playerCharacter.transform.position = new Vector3(
                (isGrounded) ? (
                Input.GetKey(KeyCode.LeftShift) ?
                (playerCharacter.transform.position.x - (float)(1.5 * speedModifier)) :
                (playerCharacter.transform.position.x - speedModifier)
                ) :
                ((playerCharacter.transform.position.x - (float)(0.5 * speedModifier))),
                playerCharacter.transform.position.y,
                playerCharacter.transform.position.z
                );
        }
		if (Input.GetKey (KeyCode.D)) {
            playerCharacter.transform.position = new Vector3(
                (isGrounded) ? (
                Input.GetKey(KeyCode.LeftShift) ?
                (playerCharacter.transform.position.x + (float)(1.5 * speedModifier)) :
                (playerCharacter.transform.position.x + speedModifier)
                ) :
                ((playerCharacter.transform.position.x + (float)(0.5 * speedModifier))),
                playerCharacter.transform.position.y,
                playerCharacter.transform.position.z
                );
        }
    }

    private void TakeDamage(int damage, string /*change to set type*/ damageType)
    {
        health -= damage/* multiply by damage resistances dictionary.damageType */;
        healthBar.fillAmount = health/maxHealth;
    }

    void OnJump()
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
