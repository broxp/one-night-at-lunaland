using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance = null;

	public float monsterDmg, safety, maxSafety, safetyDelta, phaseTwoThreshold, phaseTwoRepelThreshold, charactersOffset;
    public int ammo;
    public GameObject[] toggelables;
	public GameObject luna, teddy;
    public bool gamePaused;

    private bool phaseTwoTriggered;

    //Initialisieren als Singleton
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    //Alle Rampen in die Liste aufnehmen
    private void Start()
    {
        toggelables = GameObject.FindGameObjectsWithTag("UpRamp");
        phaseTwoTriggered = false;
        gamePaused = false;
        ammo = UberManager.instance.matches;
    }

    void Update ()
	{
        //UNTRIGGER PHASE TWO
        if(safety <= phaseTwoThreshold && !phaseTwoTriggered)
        {
            MonsterPhase2();
        }

        safety += Time.deltaTime * safetyDelta;

        UpdateCanvasView();
        UpdateLunaPosition();

	}

    //Verschiebt Luna entsprechend der Teddy-Pos
    void UpdateLunaPosition()
    {
        Vector3 lunaPos = luna.transform.position;
        lunaPos.x = teddy.transform.position.x + charactersOffset;
        luna.transform.position = lunaPos;
    }

    public void LoadLevel(int buildIndex = -1)
    {
        if(buildIndex < 0)
        {
            buildIndex = SceneManager.GetActiveScene().buildIndex;
        }

        SceneManager.LoadScene(buildIndex);
    }

    //Aktiviert alle Levelelemente die nach oben führen
    public void ActivateUpWays()
    {
        foreach (var item in toggelables)
        {
            foreach (var subItem in item.GetComponentsInChildren<Collider2D>())
            {
                subItem.enabled = true;
            }
        }
    }

    //Deaktiviert alle Levelelemente die nach oben führen
    public void DeactivateUpWays()
    {
        foreach (var item in toggelables)
        {
            foreach (var subItem in item.GetComponentsInChildren<Collider2D>())
            {
                subItem.enabled = false;
            }
        }
    }

    //Monster Abwehren
    public void RepelMonster()
    {
        if (ammo > 0)
        {
            print("light!");
            ammo--;
            AudioModell.instance.PlayAudio("match");

            if (safety <= phaseTwoRepelThreshold)
            {
                safety = maxSafety;
                AudioModell.instance.PlayAudio("repelled");
            }
            else
            {
                safety = phaseTwoThreshold;
                AudioModell.instance.PlayAudio("tooEarly");
            }
                
        }
        else
        {
            //SOUND KEINE MATCHES
            print("not enough matches");
        }
    }

    //Save Match amount
    private void OnDestroy()
    {
        UberManager.instance.matches = ammo;
    }

    //CanvasView aktualisieren
    void UpdateCanvasView()
    {
        CanvasView.instance.safety = safety;
        CanvasView.instance.ammo = ammo;
        CanvasView.instance.UpdateMatchCount(ammo);
    }

    //Wird einmal ausgeführt, wenn Phase 2 startet
    void MonsterPhase2()
    {
        AudioModell.instance.PlayAudio("monsterPhase2");
    }
}
