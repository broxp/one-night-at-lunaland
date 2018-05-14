using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nereau : MonoBehaviour
{

    public GameObject mouth;
    public AudioClip[] sounds; // set the array size and the sounds in the Inspector
    public float volume = 40;

    float frqLow = 200;
    float frqHigh = 800;
    float y0;
    private float[] freqData;
    private int nSamples = 256;
    private int fMax = 24000;
    private AudioSource audio; // AudioSource attached to this object

    float BandVol(float fLow, float fHigh)
    {
        fLow = Mathf.Clamp(fLow, 20, fMax); // limit low...
        fHigh = Mathf.Clamp(fHigh, fLow, fMax); // and high frequencies
        // get spectrum: freqData[n] = vol of frequency n * fMax / nSamples
        audio.GetSpectrumData(freqData, 0, FFTWindow.BlackmanHarris);
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
        audio = GetComponent<AudioSource>(); // get AudioSource component
        y0 = mouth.transform.position.y;
        freqData = new float[nSamples];
        audio.Play();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            PlaySoundN(0);
        }
        Vector2 _mouthPos = mouth.transform.position;
        _mouthPos.y = y0 - BandVol(frqLow, frqHigh) * volume;
        mouth.transform.position = _mouthPos;
    }

    // A function to play sound N:
    void PlaySoundN(int N)
    {
        audio.clip = sounds[N];
        audio.Play();
    }
}
