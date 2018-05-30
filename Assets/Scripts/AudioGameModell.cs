using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioGameModell : MonoBehaviour
{
    [HideInInspector]
    public List<AudioObject> audioLibrary = new List<AudioObject>();
    public static AudioGameModell instance = null;

    List<AudioObject> audioObjectsPlaying = new List<AudioObject>();

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

    public void StopAudio(string _name)
    {
        foreach(AudioObject _audioObject in audioObjectsPlaying)
        {
            if(_audioObject.name == _name)
            {
                Destroy(_audioObject.audioSourcePlayingOn);
                audioObjectsPlaying.Remove(_audioObject);
                return;
            }
        }
        print("no such audio-event with name " + _name + " found.");
    }

    public void PlayAudio(string _name, float _volume = 1, bool _isLoop = false)
    {
        AudioObject _audioObject = AudioObjectByName(_name, audioLibrary);

        if (_audioObject == null)
        {
            print("no such audio-event with name " + _name + " found.");
            return;
        }

        if (_audioObject.audioGroup.multipleClipsPossible)
        {
            StartCoroutine(InitializeAudioSource(_audioObject));
        }

        else
        {
            List<AudioObject> audioObjectsPlayingInGroup = AudioObjectsInAudioGroup(_audioObject.audioGroup, audioObjectsPlaying);

            if(audioObjectsPlayingInGroup.Count == 0)
            {
                StartCoroutine(InitializeAudioSource(_audioObject));
            }

            foreach (AudioObject _otherAudioObject in audioObjectsPlayingInGroup)
            {
                if(_otherAudioObject.importance <= _audioObject.importance)
                {
                    StartCoroutine(InitializeAudioSource(_audioObject));
                    StopAudio(_otherAudioObject.name);
                    return;
                }
            }
        }
    }

    AudioObject AudioObjectByName(string _name, List<AudioObject> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i].name == _name)
            {
                return _list[i];
            }
        }
        return null;
    }

    List<AudioObject> AudioObjectsInAudioGroup(AudioGroup _group, List<AudioObject> _list)
    {
        List<AudioObject> _audioObjects = new List<AudioObject>();

        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i].audioGroup.name == _group.name)
            {
                _audioObjects.Add(_list[i]);
            }
        }
        return _audioObjects;
    }

    IEnumerator InitializeAudioSource(AudioObject _audioObject)
    {
        AudioSource _audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        audioObjectsPlaying.Add(_audioObject);
        _audioObject.audioSourcePlayingOn = _audioSource;
        _audioSource.clip = _audioObject.audioClip;
        _audioSource.volume = _audioObject.volume;
        _audioSource.Play();

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
            if (_audioObject.importance < 2)
            {
                StopAudio(_audioObject.name);
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




