using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save4 : MonoBehaviour {

    public string nextScene;
    public AudioClip clip;

	void Start () {
        StartCoroutine(NextScene());
        AudioModell.instance.PlayAudio("music", 0.1f);
	}

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene(nextScene);
    }
}
