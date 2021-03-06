﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public int stage = 0;
    public GameObject mouth, background, shadowbox, hands, ball, eyes, lightaura;
    public AudioClip[] sounds; // set the array size and the sounds in the Inspector
    public float volume;
    public string nextScene;
    //public float mouthMoveSpeed;

    float frqLow = 200;
    float frqHigh = 800;
    float y0;
    private float[] freqData;
    private int nSamples = 256;
    private int fMax = 24000;
    private AudioSource audiosource; // AudioSource attached to this object

    public void InsertCoin()
    {
        switch (stage)
        {
            case 0:
                shadowbox.SetActive(true);
                AudioModell.instance.PlayAudio("Coin", 0.5f);
                stage++;
                break;
            case 1:
                //STROBO EFFEKTE
                hands.SetActive(true);
                ball.SetActive(true);
                AudioModell.instance.PlayAudio("Coin", 0.5f);
                stage++;
                break;
            case 2:
                //STROBO EFFEKT
                lightaura.SetActive(true);
                eyes.SetActive(false);
                AudioModell.instance.PlayAudio("Coin", 0.5f);
                stage++;
                break;
            case 3:
                //VIELE MÜNZEN SOUND
                PlaySound(0);
                StartCoroutine(NextScene());
                stage++;
                break;
        }
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(sounds[0].length);
        SceneManager.LoadScene(nextScene);
    }

    float BandVol(float fLow, float fHigh)
    {
        fLow = Mathf.Clamp(fLow, 20, fMax); // limit low...
        fHigh = Mathf.Clamp(fHigh, fLow, fMax); // and high frequencies
        // get spectrum: freqData[n] = vol of frequency n * fMax / nSamples
        audiosource.GetSpectrumData(freqData, 0, FFTWindow.BlackmanHarris);
        int n1 = Mathf.RoundToInt(Mathf.Floor(fLow * nSamples / fMax));
        int n2 = Mathf.RoundToInt(Mathf.Floor(fHigh * nSamples / fMax));
        float sum = 0;
        // average the volumes of frequencies fLow to fHigh
        for (int i = n1; i <= n2; i++)
        {
            sum += freqData[i];
        }
        return sum / (n2 - n1 + 1);
    }

    void Start()
    {
        audiosource = GetComponent<AudioSource>(); // get AudioSource component
        y0 = mouth.transform.position.y;
        freqData = new float[nSamples];
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            InsertCoin();
            
        }
        Vector3 _mouthPos = mouth.transform.position;
        _mouthPos.y = y0 - BandVol(frqLow, frqHigh) * volume;
        _mouthPos.z = mouth.transform.position.z;
        mouth.transform.position = Vector3.Lerp(_mouthPos, mouth.transform.position, 0.1f);
        //mouth.transform.position += (_mouthPos - mouth.transform.position) * Time.deltaTime * mouthMoveSpeed;
    }

    // A function to play sound N:
    void PlaySound(int _sound, float _volume = 1)
    {
        audiosource.clip = sounds[_sound];
        audiosource.volume = _volume;
        audiosource.Play();
    }
}
