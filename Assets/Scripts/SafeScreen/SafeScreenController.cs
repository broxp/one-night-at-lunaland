using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SafeScreenController : MonoBehaviour {

    float xInput, yInput;
    SafeScreenModell safeScreenModell;
    SafeScreenView safeScreenView;

	void Start ()
    {
        safeScreenView = GetComponent<SafeScreenView>();
        safeScreenModell = GetComponent<SafeScreenModell>();
	}
	
	void Update ()
    {
        if(!safeScreenView.isKicking)
        {
            xInput = CrossPlatformInputManager.GetAxis("Horizontal");
            yInput = CrossPlatformInputManager.GetAxis("Vertical");
        }
        else
        {
            xInput = 0;
            yInput = 0;
        }
    }

    private void FixedUpdate()
    {
        safeScreenModell.Move(xInput, yInput);
        safeScreenView.input.x = xInput;
        safeScreenView.input.y = yInput;
    }

    public void Kick()
    {
        safeScreenView.isKicking = true;
    }
}
