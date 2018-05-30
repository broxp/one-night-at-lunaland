using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAudioObjectsUtility : MonoBehaviour
{
    public AudioGroup audioGroup;
    public AudioClip[] audioClips;

    private void Start()
    {
        foreach(AudioClip _audioClip in audioClips)
        {
            CreateAudioObject(_audioClip, audioGroup);
        }
    }

    public void CreateAudioObject(AudioClip _audioClip, AudioGroup _audioGroup)
    {
        AudioObject _audioObject = ScriptableObject.CreateInstance<AudioObject>();

        _audioObject.audioClip = _audioClip;
        _audioObject.audioGroup = _audioGroup;

        string path = "Assets/ScriptableObjects/AudioObjects/";
        //string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        //if (path == "")
        //{
        //    path = "Assets";
        //}
        //else if (Path.GetExtension(path) != "")
        //{
        //    path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        //}

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + _audioClip.name + ".asset");

        AssetDatabase.CreateAsset(_audioObject, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = _audioObject;
    }
}