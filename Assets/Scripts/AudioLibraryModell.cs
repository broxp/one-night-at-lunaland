using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioLibraryModell : MonoBehaviour {

    [SerializeField]
    public AudioObject[] audioObjects;

    public void LoadLibraryIntoManager()
    {
        foreach (AudioObject _audioObject in audioObjects)
        {
            AudioGameModell.instance.audioLibrary.Add(_audioObject);
        }
    }
}
