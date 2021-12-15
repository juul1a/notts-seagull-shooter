﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM9 : MonoBehaviour {

	public float health;
	public float maxHealth;
	public float movementSpeed;
	public float groundRadius;
	public float jumpForce;
	public bool airControl;
	public bool cameraFollow;
	public Transform[] GroundPoints;
	public LayerMask whatIsGround;
	public Color hurtColor;


	public bool facingRight;
	public bool isGrounded;
	private bool jump;
	private Rigidbody2D playerRigidBody; 
	private Vector3 respawnPoint;
	private GameObject mainCamera;
	public bool dead = false;
	public Animator anim;
	private GameObject staticBG;

	void Start () {
		facingRight = true;
		playerRigidBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		if(anim == null){
			anim = GetComponentsInChildren<Animator>()[0];
		}
		mainCamera = GameObject.Find ("Main Camera");
		staticBG = GameObject.Find ("clouds");
		respawnPoint = playerRigidBody.position;
	}

	void Update(){
		if(StateManager.smInstance.IsPlaying()){
			if(!playerRigidBody.simulated){
				playerRigidBody.simulated = true;
			}
			InputHandler ();
		}	
	}

	void FixedUpdate() {
		if(StateManager.smInstance.IsPlaying()){
			float horizontal = Input.GetAxis ("Horizontal");
			//isGrounded = IsGrounded ();
			// HandleLayers ();
			HandleMovement (horizontal);
			Backwards(horizontal);
			//Flip (horizontal);
			ResetValues ();
		}

	}

	void InputHandler(){
		if(Input.GetKeyDown(KeyCode.Space)){
			jump = true;
		}
	}

	private void HandleMovement (float horizontal) {
		if(!dead){
			if(isGrounded || airControl){
				playerRigidBody.velocity = new Vector2(horizontal*movementSpeed, playerRigidBody.velocity.y); // x = -1, y = 0
				// if (cameraFollow) {
				// 	// mainCamera.transform.position = new Vector3 (playerRigidBody.transform.position.x, playerRigidBody.position.y, mainCamera.transform.position.z);
				// 	staticBG.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, staticBG.transform.position.z);
				// }

			}
			if(isGrounded && jump){
				//isGrounded = false;
				AudioManager.audioManager.Play("Jump Up");
				playerRigidBody.AddForce(new Vector2(0, jumpForce));
			}
			// if(!isGrounded){
			// 	//velocity positive
			// 	if(playerRigidBody.velocity.y > 0){
			// 		anim.SetBool("Jumping Up", true);
			// 	}
			// 	else if(playerRigidBody.velocity.y < 0){
			// 		//velocity negative
			// 		anim.SetBool("Jumping Up", false);
			// 	}
			// }
			anim.SetFloat ("Speed", Mathf.Abs(horizontal));
		}
		
	}

	private void Backwards(float horizontal){
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			// Debug.Log("Backwards");
			anim.SetBool ("Backwards", true);
		}
		else{
			// Debug.Log("Forwards");
			anim.SetBool ("Backwards", false);
		}
	}

	private void Flip(float horizontal){
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			transform.Rotate(0f, 180f, 0f);
		}
	}

	public void Flip2(){
		transform.Rotate(0f, 180f, 0f);
		facingRight = !facingRight;
	}

	public void Heal(float heal){
		if(heal + health > maxHealth){
			health = maxHealth;
		}
		else{
			health += heal;
		}
	}
	
	public void TakeDamage(float damage){
		if(!dead){
			health -= damage;
			StartCoroutine ("HurtColor");
			// Debug.Log("Player is hit!");
			if(health <=0){
				KO();
			}
		}
	}

	public float GetHealth(){
		return health;
	}

	public void KO(){
		if(!dead){
			dead = true;
			anim.SetTrigger("Dead");
			StateManager.smInstance.SetState(StateManager.State.Lose);
		}
		
		// playerRigidBody.constraints = RigidbodyConstraints2D.None;
		// playerRigidBody.AddTorque(3);
	}

	// private bool IsGrounded(){
	// 	foreach (Transform point in GroundPoints) {
	// 		//Make a circle collider over every ground point
	// 		Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
	// 		for (int i = 0; i < colliders.Length; i++) {
	// 			//If collider != player, so player does not collide with itself
	// 			if(colliders[i].gameObject != gameObject){
	// 				anim.SetBool ("Jumping Up", false);
	// 				anim.SetBool ("Landing", false);
	// 				anim.SetBool ("Ground", true);
	// 				return true;
	// 			}
	// 		}
	// 	}
	// 	anim.SetBool ("Ground", false);
	// 	return false;
	// }

	//For animator (in air vs on ground)
	// private void HandleLayers(){
	// 	if(!isGrounded){
	// 		anim.SetLayerWeight(1,1);
	// 		anim.SetLayerWeight(0,0);
	// 	}
	// 	else{
	// 		anim.SetLayerWeight(1,0);
	// 		anim.SetLayerWeight(0,1);
	// 	}	
	// }

	void ResetValues(){
		jump = false;
	}

	void Respawn (){
		playerRigidBody.position = respawnPoint;
	}

	void OnTriggerEnter2D(Collider2D triggerEntered){
		if (triggerEntered.tag == "Respawn") {
			Respawn ();
		}
	}

	IEnumerator HurtColor() {
		for (int i = 0; i < 3; i++) {
			SpriteRenderer[] SPRs = GetComponentsInChildren<SpriteRenderer>();
			foreach(SpriteRenderer SPR in SPRs){
				SPR.color = hurtColor;
			}
			yield return new WaitForSeconds (.1f);
			foreach(SpriteRenderer SPR in SPRs){
				SPR.color = Color.white; //White is the default "color" for the sprite, if you're curious.
			}
			yield return new WaitForSeconds (.1f);
		}
	} //This IEnumerator runs 3 times, resulting in 3 flashes.

}