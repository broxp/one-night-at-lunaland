using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeScreenLogic : MonoBehaviour {

    public string VoiceClipAtLevelStart;
    public bool playMusic = true;

	void Start () {
		if(VoiceClipAtLevelStart != "")
        {
            AudioModell.instance.PlayAudio(VoiceClipAtLevelStart);
        }
        if (playMusic)
        {
            AudioModell.instance.PlayAudio("music", 0.3f);
        }
    }
}
