using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioObject", menuName = "AudioObject", order = 1)]
public class AudioObject : ScriptableObject
{
    [RangeAttribute(0, 1)]
    public float volume = 1f;
    [RangeAttribute(0, 2)]
    public int importance;
    public AudioGroup audioGroup;
    public AudioClip audioClip;
    public bool loop = false;
    [HideInInspector]
    public AudioSource audioSourcePlayingOn;
}
