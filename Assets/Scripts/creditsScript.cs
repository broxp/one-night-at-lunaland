using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsScript : MonoBehaviour {

    public float scrollSpeed;

    void Update()
    {
        Vector2 _temp = gameObject.transform.position;
        _temp.y -= scrollSpeed * Time.deltaTime;
        gameObject.transform.position = _temp;

        if(transform.position.y < -24.4)
        {
            Application.Quit();
        }
    }
}
