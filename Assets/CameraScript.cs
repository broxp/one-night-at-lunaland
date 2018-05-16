using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform Player;

    Vector3 offset;

	void Start () {
        offset = Player.position - gameObject.transform.position;
	}
	
	void Update () {
        gameObject.transform.position = Player.position - offset;
	}
}
