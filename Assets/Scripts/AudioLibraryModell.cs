using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibraryModell : MonoBehaviour {

    public List<AudioObject> audioObjects = new List<AudioObject>();
    public AudioLibraryModell instance = null;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else if (instance != this)
        {
            instance.audioObjects.AddRange(audioObjects);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioGameModell.instance.audioLibrary = audioObjects;
    }
}
