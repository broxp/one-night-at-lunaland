﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class TeddyController : MonoBehaviour {

    private TeddyModell teddyModell;
    private TeddyView teddyView;


    private void Awake()
    {
        teddyView = GetComponent<TeddyView>();
        teddyModell = GetComponent<TeddyModell>();
    }


    private void Update()
    {
        if(LevelManager.instance.gameOver == true)
        {
            if(Input.GetButtonDown("Repel"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if(CrossPlatformInputManager.GetButtonDown("Repel"))
        {
            teddyModell.ActivateLight();
        }
        if(LevelManager.instance.gamePaused == true)
        {
            if (CrossPlatformInputManager.GetAxis("Vertical") != 0)
            {
                if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
                {
                    teddyModell.GoDown();
                }
                if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
                {
                    teddyModell.GoUp();
                }
            }
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        if (LevelManager.instance.gamePaused)
        {
            crouch = false;
            h = 0;
        }

        // Pass all parameters to the character control script.
        teddyView.input.x = h;
        teddyModell.Move(h);
    }
}

