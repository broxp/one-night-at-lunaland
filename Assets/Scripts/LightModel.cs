using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightModel : MonoBehaviour
{

    [Range(0.05f, 0.2f)]
    public float flickTime;

    [Range(1.02f, 1.09f)]
    public float addSize;

    float timer = 0;
    bool bigger = true;

    float originalXScale;

    private void Start()
    {
        originalXScale = transform.localScale.x;
    }

    void Update()
    {
        Flicker();
    }

    void Flicker()
    {
        timer += Time.deltaTime;

        if (timer > flickTime)
        {
            if (bigger)
            {
                transform.localScale = new Vector3(originalXScale * addSize, originalXScale * addSize, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(originalXScale, originalXScale, transform.localScale.z);
            }
            timer = 0;
            bigger = !bigger;
        }
    }
}