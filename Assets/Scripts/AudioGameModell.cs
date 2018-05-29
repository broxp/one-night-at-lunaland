using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioGameModell : MonoBehaviour
{
    public List<AudioObject> audioLibrary = new List<AudioObject>();
    public static AudioGameModell instance = null;
    public AudioGroup[] audioGroups;

    List<AudioObject> audioObjectsPlaying = new List<AudioObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }



    public void PlayAudio(string _name, float _volume = 1, bool _isLoop = false)
    {
        AudioObject _audioObject = AudioObjectByName(_name, audioLibrary);

        if (_audioObject == null)
        {
            print("no such audio-event found");
            return;
        }

        if (_audioObject.audioGroup.multipleClipsPossible)
        {
            StartCoroutine(InitializeAudioSource(_audioObject));
        }

        else
        {
            List<AudioObject> audioObjectsPlayingInGroup = AudioObjectsInAudioGroup(_audioObject.audioGroup, audioObjectsPlaying);

            foreach (AudioObject _otherAudioObject in audioObjectsPlayingInGroup)
            {
                if(_otherAudioObject.importance <= _audioObject.importance)
                {
                    StartCoroutine(InitializeAudioSource(_audioObject));
                    Destroy(_otherAudioObject.audioSourcePlayingOn);
                    audioObjectsPlaying.Remove(_otherAudioObject);
                    return;
                }
            }
        }
    }

    AudioObject AudioObjectByName(string _name, List<AudioObject> _list)
    {
        for (int i = 0; i <= _list.Count; i++)
        {
            if (_list[i].name == _name)
            {
                return _list[i];
            }
        }
        print("AudioObject not found");
        return null;
    }

    List<AudioObject> AudioObjectsInAudioGroup(AudioGroup _group, List<AudioObject> _list)
    {
        List<AudioObject> _audioObjects = new List<AudioObject>();

        for (int i = 0; i <= _list.Count; i++)
        {
            if (_list[i].audioGroup == _group)
            {
                _audioObjects.Add(_list[i]);
            }
        }

        return _audioObjects;
    }

    IEnumerator InitializeAudioSource(AudioObject _audioObject)
    {
        AudioSource _audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        _audioObject.audioSourcePlayingOn = _audioSource;
        _audioSource.clip = _audioObject.audioClip;
        _audioSource.Play();
        _audioSource.volume = _audioObject.volume;

        if (_audioObject.loop)
        {
            _audioSource.loop = true;
        }
        else
        {
            yield return new WaitForSeconds(_audioSource.clip.length);
            Destroy(_audioSource);

        }
    }

    public void EndLowImportanceClips()
    {
        foreach (AudioObject _audioObject in audioObjectsPlaying)
        {
            if (_audioObject.importance < 3)
            {
                Destroy(_audioObject.audioSourcePlayingOn);
                audioObjectsPlaying.Remove(_audioObject);
            }
        }
    }

    //int SearchListIndexForObject<T>(ref T _object, List<T> _list)
    //{
    //    for (int x = 0; x <= _list.Count; x++)
    //    {
    //        if (_list[x].Equals(_object))
    //        {
    //            return x;
    //        }
    //    }
    //    print("object not found");
    //    return 0;
    //}
}

public class AudioObject
{
    public string name;
    [RangeAttribute(1, 3)]
    public int importance;
    public AudioGroup audioGroup;
    public AudioClip audioClip;
    public float volume = 1f;
    public bool loop = false;
    public AudioSource audioSourcePlayingOn;
}

public class AudioGroup
{
    public string name;
    public bool multipleClipsPossible = true;
}
