using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public float hp = 0;
    public int matches = 1;
    public static GameManagerScript instance = null;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioGameModell.instance.EndOfSceneCleanup();
        LoadAudioLibraries();    
    }

    private void Update()
    {
        if(Input.GetButton("Skip"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(Input.GetButtonDown("Last"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    void LoadAudioLibraries()
    {
        foreach (GameObject _gameObject in GameObject.FindGameObjectsWithTag("AudioLibrary"))
        {
            _gameObject.GetComponent<AudioLibraryModell>().LoadLibraryIntoManager();
        }
    }
}
