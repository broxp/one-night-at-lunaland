using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class TeddyController : MonoBehaviour {

    private TeddyModell teddyModell;
    private PlatformerCharacter2D m_Character;
    private bool m_Jump;


    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
        teddyModell = GetComponent<TeddyModell>();
    }


    private void Update()
    {
        //if (!m_Jump)
        //{
        // Read the jump input in Update so button presses aren't missed.
        //    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        //}
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        if (GameLogic.instance.gamePaused)
        {
            crouch = false;
            h = 0;
        }

        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump);
        m_Jump = false;
    }
}

