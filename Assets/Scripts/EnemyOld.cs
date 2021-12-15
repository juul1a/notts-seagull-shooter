using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOld : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage){
        Debug.Log("Oh Owwie!");
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Color randomColor = new Color(Random.Range(0,255),Random.Range(0,255), Random.Range(0,255));
        sr.color = randomColor;

    }
}
