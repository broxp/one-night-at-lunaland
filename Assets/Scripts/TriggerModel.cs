using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TriggerModel : MonoBehaviour {

    public enum EventType { Audio, Scene};

    public EventType eventType;
    public string EventName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(eventType == EventType.Audio)
        {
            AudioModell.instance.PlayAudio(EventName);
            Destroy(this);
        }
        else if(eventType == EventType.Scene)
        {
            SceneManager.LoadScene(EventName);
        }
    }
}
