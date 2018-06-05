using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save4 : MonoBehaviour {

    public float zoomSpeed;
    public GameObject mainCam;
    public string nextScene;
    public AudioClip clip;

    Camera mainCamScript;

	void Start () {
        StartCoroutine(NextScene());
        mainCamScript = mainCam.GetComponent<Camera>();
        AudioModell.instance.PlayAudio("music", 0.1f);
	}

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene(nextScene);
    }

    private void Update()
    {
        mainCamScript.orthographicSize -= zoomSpeed * Time.deltaTime;
    }
}
