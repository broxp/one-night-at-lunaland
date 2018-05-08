using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance = null;

	public double ammo, monsterDmg, safety, maxSafety, safetyDelta, phaseTwoThreshold;
    private bool phaseTwoTriggered = false;
	public List<GameObject> toggelable;

	public GameObject luna, teddy;
	public float charactersOffset;

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

    //Aktiviert alle Levelelemente die nach oben führen
    public void ActivateUpWays()
    {
        foreach (var item in toggelable)
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
        foreach (var item in toggelable)
        {
            foreach (var subItem in item.GetComponentsInChildren<Collider2D>())
            {
                subItem.enabled = false;
            }
        }
    }

    //CanvasView aktualisieren
    void UpdateCanvasView()
    {
        CanvasView.instance.safety = safety;
        CanvasView.instance.ammo = ammo;
    }

    //Wird einmal ausgeführt, wenn Phase 2 startet
    void MonsterPhase2()
    {
        //SOUND WECHSELN
    }

    public void ResetToCheckpoint()
    {
        //LUNA STIRBT
    }
}
