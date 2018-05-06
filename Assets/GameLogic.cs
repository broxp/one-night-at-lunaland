using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
	public Text txt;
	public double hp, ammo, safety;
	public double safetyDelta;
	public List<GameObject> toggelable;
	public bool isOnDecision;

	void Start ()
	{
	}

	void Update ()
	{
		safety += Time.deltaTime * safetyDelta;

		txt.text = "HP: " + hp + "\n"
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
