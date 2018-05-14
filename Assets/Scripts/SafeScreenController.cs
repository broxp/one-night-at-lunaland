using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SafeScreenController : MonoBehaviour {

    float xInput, yInput;
    SafeScreenModell safeScreenModell;

	void Start ()
    {
        safeScreenModell = GetComponent<SafeScreenModell>();
	}
	
	void Update ()
    {
		xInput = CrossPlatformInputManager.GetAxis("Horizontal");
        yInput = CrossPlatformInputManager.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        safeScreenModell.Move(xInput, yInput);
    }
}
