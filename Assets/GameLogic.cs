using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
	public Text txt;
	public double hp, ammoCarriedByTeddy, ammo, safety;
	public double safetyDelta;
	public List<GameObject> toggelable;
	public bool isOnDecision;

	public GameObject luna, teddy;
	public float charactersOffset;
	void Start ()
	{
	}

	void Update ()
	{
		safety += Time.deltaTime * safetyDelta;

		var lunaPos = luna.transform.position;
		lunaPos.x = teddy.transform.position.x + charactersOffset;
		luna.transform.position = lunaPos;

		txt.text = "HP: " + hp + "\n"
		+ "ammoCarriedByTeddy: " + ammoCarriedByTeddy + "\n"
		+ "Ammo: " + ammo + "\n"
		+ "Safety: " + safety + "\n"
		+ "isOnDecision: " + isOnDecision;


		if (isOnDecision && Input.GetKeyDown ("space")) {
			foreach (var item in toggelable) {
				foreach (var subItem in item.GetComponentsInChildren<Collider2D> ()) {
					subItem.enabled = !subItem.enabled;
				}
			}
		}
	}
}
