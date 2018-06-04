using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAudio : MonoBehaviour {

    bool triggered = false;

    private void Start()
    {
        AudioGameModell.instance.PlayAudio("whenIsaynow");
    }

    void Update () {
		if(LevelManager.instance.safety < LevelManager.instance.phaseTwoRepelThreshold && !triggered)
        {
            AudioGameModell.instance.PlayAudio("now");
            triggered = true;
        }
	}
}
