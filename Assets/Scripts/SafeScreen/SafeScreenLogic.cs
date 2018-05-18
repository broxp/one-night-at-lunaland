using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeScreenLogic : MonoBehaviour {

    public string VoiceClipAtLevelStart;

	void Start () {
		if(VoiceClipAtLevelStart != "")
        {
            AudioModell.instance.PlayAudio(VoiceClipAtLevelStart);
        }
	}
}
