using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioGroup", menuName = "AudioGroup", order = 1)]
public class AudioGroup : ScriptableObject
{
    public bool multipleClipsPossible = true;
}
