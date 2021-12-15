using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileM9 : MonoBehaviour {

	public float speed = 100;
	public float damage = 10;
	// private float directionX = 1;
	public Rigidbody2D bulletBod;
	public float lifespan = 4;
	

	// Use this for initialization
	void Start () {
		bulletBod.velocity = transform.right * speed;
		//Invokes explode function after the lifespan duration
		Invoke("Explode", lifespan);
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "EnemyHitBox"){
			Enemy enemy = col.GetComponentInParent<Enemy>();
			if(enemy != null){
				enemy.TakeDamage(damage);
			}
			Explode();
		}
	}

	void Explode(){
		Destroy(gameObject);
	}
}
