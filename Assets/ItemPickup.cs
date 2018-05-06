using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {
	public GameLogic game;
	// Use this for initialization
	void Start () {
		game = GameObject.FindGameObjectsWithTag("Logic")[0].GetComponent<GameLogic>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		print ("item picked up");
		game.ammoCarriedByTeddy++;
		Destroy (gameObject);
	}

}