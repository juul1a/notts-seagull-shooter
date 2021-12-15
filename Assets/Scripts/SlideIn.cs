using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideIn : MonoBehaviour
{
    private Vector3 startPos;
    public Vector3 endPos;
    public float waitTime = 2f;

    private string status;
    private float timer = 0;

    void Awake(){
        startPos = transform.position;
    }
    
    public void SlideMeIn(){
        status = "sliding in";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(status)
        {
            case "sliding in":
                transform.position = Vector3.Lerp (transform.position, endPos, Time.fixedDeltaTime);
                if(Vector3.Distance(transform.position, endPos)<3){
                    status = "waiting";
                    timer = waitTime;
                }
                break;
            case "waiting":
                timer -= Time.fixedDeltaTime;
                if(timer <= 0){
                    status = "sliding out";
                    timer = 0;
                }
                break;
            case "sliding out":
                transform.position = Vector3.Lerp (transform.position, startPos, Time.fixedDeltaTime);
                if(Vector3.Distance(transform.position, startPos)<3){
                    status = "";
                }
                break;

        }
    }
}
