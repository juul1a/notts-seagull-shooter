using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterM9 : MonoBehaviour {

	public float fireRate = 0;
	float timeToFire = 0;
	public float fireDelay = 0.2f;
	public GameObject bulletPrefab;
	private PlayerM9 player;

	// Use this for initialization
	void Awake () {
		player = GetComponentInParent<PlayerM9>();
	}
	
	// Update is called once per frame
	void Update () {
		//Shoot();
		//Single burst
		if(fireRate == 0){
			if(Input.GetButtonDown("Fire1")){
				if(!(player.anim.GetFloat("Speed")>0)){
					player.anim.SetTrigger("Shoot");
					Invoke("Shoot", fireDelay);
				}
				else{
					Shoot();
				}
			}
		}
		//Automatic
		else{
			if(Input.GetButtonDown("Fire1") && Time.time > timeToFire){
				timeToFire = Time.time + 1/fireRate;
				if(!(player.anim.GetFloat("Speed")>0)){
					//only do shoot animation if we're idle
					player.anim.SetTrigger("Shoot");
					Invoke("Shoot", fireDelay);
				}
				else{
					Shoot();
				}
				
			}
		}
	}

	void Shoot(){
		if(!player.dead){
			//  Debug.Log("SHOOTIN");
			AudioManager.audioManager.Play("Bolt");
			Instantiate(bulletPrefab, transform.position, transform.rotation);
		}
		
		
		
		
		//Projects screen co-ordinates of mouse into world position
		//Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		//RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit); //params are Origin, directio (= final point - origin), distance, layermask of what not to hit
		//Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition)*100, Color.black);
		//if(hit.collider != null){
		//	 Debug.DrawLine(firePointPosition, hit.point, Color.red);
		//}
	}
}
