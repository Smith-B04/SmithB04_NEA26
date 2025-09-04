using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

	public float speedModifer;
	public float jumpModifier;
	public GameObject playerCharacter;
	private Rigidbody2D rb;
	private bool sprint;

	// Use this for initialization
	void Start () {
		rb = playerCharacter.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.A)) {
			playerCharacter.transform.position = new Vector3 (playerCharacter.transform.position.x - 0.1f, playerCharacter.transform.position.y, playerCharacter.transform.position.z);
		}
		if (Input.GetKey (KeyCode.D)) {
			playerCharacter.transform.position = new Vector3 (playerCharacter.transform.position.x + 0.1f, playerCharacter.transform.position.y, playerCharacter.transform.position.z);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.AddForce (new Vector3 (0, jumpModifier, 0));
		}
	}
}
