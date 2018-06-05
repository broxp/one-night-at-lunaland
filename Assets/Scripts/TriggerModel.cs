using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TriggerModel : MonoBehaviour {

    public enum EventType { Audio, Scene, Toggle};

    public EventType eventType;
    public string EventName;
    public AudioObject audioObject;
    public GameObject toggleObject;
    public float volume = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(eventType == EventType.Audio)
        {
            AudioModell.instance.PlayAudio(EventName, volume);
            Destroy(this.gameObject);
        }
        else if(eventType == EventType.Scene)
        {
            SceneManager.LoadScene(EventName);
        }
        else if(eventType == EventType.Toggle)
        {
            toggleObject.SetActive(!toggleObject.activeSelf);
            Destroy(this.gameObject);
        }
    }
}
