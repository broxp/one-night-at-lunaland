using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

	public float safety, maxSafety, phaseTwoThreshold, phaseTwoRepelThreshold, charactersOffset;
    public int ammo;
    [HideInInspector]
    public GameObject[] toggelables;
    [HideInInspector]
    public float monsterDmg;
    public bool gamePaused;

    float safetyDelta;
    GameObject luna, teddy;
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
        AudioGameModell.instance.PlayAudio("LevelMusic", 0.3f);
        toggelables = GameObject.FindGameObjectsWithTag("UpRamp");
        luna = GameObject.FindGameObjectWithTag("Luna");
        teddy = GameObject.FindGameObjectWithTag("Player");
        phaseTwoTriggered = false;
        gamePaused = false;
        if (GameManagerScript.instance != null)
        {
            ammo = GameManagerScript.instance.matches;
        }
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
            AudioGameModell.instance.PlayAudio("Match");
            phaseTwoTriggered = false;

            if (safety <= phaseTwoRepelThreshold)
            {
                safety = maxSafety;
                AudioGameModell.instance.PlayAudio("MonsterRepelled");
                AudioGameModell.instance.StopAudio("MonsterBreath");
            }
            else if (phaseTwoRepelThreshold <= safety && safety <= phaseTwoThreshold)
            {
                safety = phaseTwoThreshold;
                AudioGameModell.instance.PlayAudio("MonsterAnnoyed");
                AudioGameModell.instance.StopAudio("MonsterBreath");
            }
            else if (safety > phaseTwoThreshold)
            {
                //UNNÖTIG GEZÜNDETES STREICHHOLZ
            }
                
        }
        else
        {
            AudioGameModell.instance.PlayAudio("Matchbox");
            print("not enough matches");
        }
    }

    //Save Match amount
    private void OnDestroy()
    {
        if(GameManagerScript.instance != null)
        {
            GameManagerScript.instance.matches = ammo;
        }
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
        AudioGameModell.instance.PlayAudio("MonsterBreath");
        phaseTwoTriggered = true;
    }
}
