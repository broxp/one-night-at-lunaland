using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {

    public string audioEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioModell.instance.PlayAudio(audioEvent);
        Destroy(this);
    }
}
