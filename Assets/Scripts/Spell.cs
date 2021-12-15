using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
		public float castRange = 30;
		public float castRate = 0.1f;
		float coolDownTime;
		public float damage = 10;
		public GameObject auraPrefab;
		public Color auraColour;
		public float spellDuration = 5;
        public Animation castAnim;
		public bool cooledDown = true;

	public void Animate(PlayerM9 player){
		Debug.Log("Animating player");
	}


	void StartCoolDown(){
		cooledDown = false;
		//count down. coroutine???
		cooledDown = true;
	}


}