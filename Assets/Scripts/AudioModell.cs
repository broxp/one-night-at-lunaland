using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioModell : MonoBehaviour {

    public static AudioModell instance = null;

    [SerializeField]
    AudioEvent[] audioEvents;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudio(string _name, float _volume = 1, bool _isLoop = false)
    {
        for (int i = 0; i <= audioEvents.Length; i++)
        {
            if(audioEvents[i].name == _name)
            {
                AudioSource _audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
                _audioSource.clip = audioEvents[i].GetClip();
                _audioSource.Play();
                _audioSource.volume = _volume;

                if (_isLoop)
                {
                    _audioSource.loop = true;
                }
                else
                {
                    Destroy(_audioSource, _audioSource.clip.length);
                }
                return;
            }
        }
        print("no such audio-event found");
    }
}

[System.Serializable]
public class AudioEvent
{
    //[Range(0f, 1f)]
    //public float triggerChance = 1f;
    public string name;
    
    [SerializeField]
    private AudioClip[] audioSources;

    public AudioClip GetClip()
    {
        //if(Random.Range(0, 1) <= triggerChance)
        if (Random.Range(0, 1) <= 1)
        {
            return (audioSources[Random.Range(0, audioSources.Length)]);
        }
        else
        {
            return null;
        }
    }
}
