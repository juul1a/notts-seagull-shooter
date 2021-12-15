using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {

	private float damage;
	private Enemy thisEnemy;
	//get parent (enemy)
	//only do damage if parent state = attacking


	// public LayerMask hittable; //In the future maybe enemies can hit more than just players EH?? like buttons n stuff
	// Use this for initialization
	void Start () {
		thisEnemy = gameObject.GetComponentInParent<Enemy>();
		damage = thisEnemy.damage;
	}

	void OnTriggerEnter2D(Collider2D hitItem){
		if(thisEnemy == null)
		{
			thisEnemy = gameObject.GetComponentInParent<Enemy>();
		}
		if(thisEnemy.attacking && !thisEnemy.isDead()){
			PlayerM9 player = hitItem.GetComponentInParent<PlayerM9>();
			if (player != null){
				player.TakeDamage(damage);
			}
		}
	}
	
}
