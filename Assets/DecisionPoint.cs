using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionPoint : MonoBehaviour {
	public GameLogic game;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		print (other + "yay");
		game.isOnDecision = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		print (other + "nay");
		game.isOnDecision = false;
	}
}