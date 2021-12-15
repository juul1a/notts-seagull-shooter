using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerM9 player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerM9>();
    }

    void OnTriggerEnter2D(Collider2D col){        
        if(col.tag == "Ground"){
            player.isGrounded = true;
            player.anim.SetTrigger("Landing");
            player.anim.SetBool("Jumping", false);
            AudioManager.audioManager.Play("Land");
        }
       // player.anim.SetLayerWeight(1,0);
		//player.anim.SetLayerWeight(0,1);
	}
	// void OnTriggerStay2D(Collider2D col){
	// 	player.isGrounded = true;
	// }

	void OnTriggerExit2D(Collider2D col){
        if(col.tag == "Ground"){
            player.isGrounded = false;
            player.anim.SetBool("Jumping", true);
        }
       // player.anim.SetLayerWeight(1,1);
       //player.anim.SetLayerWeight(0,0);
	}
}
