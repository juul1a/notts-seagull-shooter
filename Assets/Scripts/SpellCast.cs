using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : MonoBehaviour
{

   
	public GameObject[] Spells;

	private PlayerM9 player;

	void Start(){
		player = gameObject.GetComponentInParent<PlayerM9>();
	}
	
	// Update is called once per frame
	void Update () {
		Spell spell = Spells[0].GetComponent<Spell>();

		 if(spell.cooledDown){
			 Debug.Log("About to cast");
			if(Input.GetButtonDown("Fire2")){
				Debug.Log("Clicked");
				Enemy baddie = BaddieInRange(spell);
				if(baddie != null){
					Cast(baddie, spell);
				}
			}
		}
		//Automatic
		// else{
		// 	if(Input.GetButtonDown("Fire1") && Time.time > spell.timeToCast){
		// 		spell.timeToCast = Time.time + 1/castRate;
		// 		Shoot();
		// 	}
		// 	if(Input.GetButtonDown("Fire2") && Time.time > timeToCast){
		// 		timeToCast = Time.time + 1/castRate;
		// 		Enemy baddie = BaddieInRange();
		// 		if(baddie != null){
		// 			Cast(baddie);	
		// 		}
		// 	}
		// }
	}

	Enemy BaddieInRange(Spell spell){
		Debug.Log("Hey");
		RaycastHit2D baddieHit = Physics2D.CircleCast(transform.position, spell.castRange, Vector3.right);
		if(baddieHit.transform.tag == "Enemy") {
			bool infrontBaddieLeft = player.facingRight && (baddieHit.transform.position.x - transform.position.x ) > 0 ;
			bool infrontBaddieRight = !player.facingRight && (transform.position.x - baddieHit.transform.position.x) > 0;
			bool attackRange = Mathf.Abs(baddieHit.transform.position.x - transform.position.x)<=spell.castRange;
			if((infrontBaddieLeft || infrontBaddieRight) && attackRange){
				Debug.Log("Baddie is in range!");
				Enemy enemy = baddieHit.transform.gameObject.GetComponentInParent<Enemy>();
				return enemy;
			}
		}
		Debug.Log("Baddie is NAHT in range!");
		return null;
	}

	void Cast(Enemy baddie, Spell spell){
		
		baddie.TakeDamage(spell.damage);
		spell.Animate(player);
		// Vector3 auraPos = new Vector3(baddie.transform.position.x, baddie.transform.position.y-7, baddie.transform.position.z);
		// GameObject aura = Instantiate(auraPrefab, auraPos, baddie.transform.rotation);
		// AuraController auraCon = aura.GetComponent<AuraController>();
		// auraCon.timeout = spellDuration;
		// aura.transform.parent = baddie.transform;
		Debug.Log("Casting");
	}
}

