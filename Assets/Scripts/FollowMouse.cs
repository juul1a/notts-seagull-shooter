using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    private Vector3 cameraPos, mouseDir;
    private PlayerM9 nott;

    void Awake(){
        nott = gameObject.GetComponentInParent<PlayerM9>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!nott.dead && StateManager.smInstance.IsPlaying()){
            cameraPos = Camera.main.WorldToScreenPoint(transform.position);
            mouseDir = Input.mousePosition - cameraPos;
            float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            // Debug.Log("Rotating by angle "+angle);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if(mouseDir.x > transform.position.x && !nott.facingRight){
                // Debug.Log("FLIP RIGHT");
                nott.Flip2();
            }
            if(mouseDir.x < transform.position.x && nott.facingRight){
                // Debug.Log("FLIP LEFT");
                nott.Flip2();
            }
        }   
    }
}
