using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeddyModell : MonoBehaviour {

    public int ammoCarriedByTeddy;
    public float hp, movementForce;
    public bool safe = true;

    bool sufferingAudioPlaying = false;

    Rigidbody2D rigidbody;

    private void Start()
    {
        safe = false;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateCanvasView();

        if (LevelManager.instance.safety <= 0)
        {
            if(safe || hp <= 0)
            {
                LevelManager.instance.RestartGame();
            }
            else
            {
                hp -= LevelManager.instance.monsterDmg * Time.deltaTime;
                if (!sufferingAudioPlaying)
                {
                    AudioGameModell.instance.PlayAudio("TeddySuffering");
                    sufferingAudioPlaying = true;
                }
            }
        }
    }


    //Registriert, wenn Kreuzung, Pickup, Rampenvon der "falschen" Seite, oder Lunas Sicherheitsbereich betreten wurde
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            print("Item");
            PickItemUp(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Crossing")
        {
            print("crossing");
            CanvasView.instance.ShowCrossingBubble();
            LevelManager.instance.gamePaused = true;
        }

        if(collision.gameObject.tag == "UpRamp")
        {
            LevelManager.instance.DeactivateUpWays();
        }

        if (collision.gameObject.tag == "Luna")
        {
            BackToLuna();
        }
    }

    //Löscht alle Sprechblasen beim verlassen interaktiver Zone und registriert verlassen von Lunas sicherer Zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            CanvasView.instance.ClearBubbles();
        }

        if (collision.gameObject.tag == "Luna")
        {
            safe = false;
        }
    }

    //Canvas View Aktualisieren
    void UpdateCanvasView()
    {
        CanvasView.instance.ammoCarriedByTeddy = ammoCarriedByTeddy;
        CanvasView.instance.hp = hp;
    }

    public void Move(float _moveVector)
    {
        rigidbody.velocity = (new Vector2(_moveVector * movementForce, rigidbody.velocity.y));
    }

    //Nach oben gehen
    public void GoUp()
    {
        print("up!");
        LevelManager.instance.gamePaused = false;
        LevelManager.instance.ActivateUpWays();
        CanvasView.instance.ClearBubbles();
        //AudioGameModell.instance.PlayAudio("suffering");
    }

    //Nach unten gehen
    public void GoDown()
    {
        print("down!");
        LevelManager.instance.gamePaused = false;
        LevelManager.instance.DeactivateUpWays();
        CanvasView.instance.ClearBubbles();
    }

    //Item aufheben
    public void PickItemUp(GameObject item)
    {
        print("picked up!");
        GameObject.Destroy(item);
        //ammoCarriedByTeddy++;
        LevelManager.instance.ammo++;
        CanvasView.instance.ClearBubbles();
    }

    //Item liegen lassen
    public void LeaveItem()
    {
        print("left item");
        CanvasView.instance.ClearBubbles();
    }

    //Aktionen wenn Teddy zurück in Lunas Sicherheit ist LUNA TRIGGER EINRICHTEN
    void BackToLuna()
    {
        print("back!");

        //AudioGameModell.instance.PlayAudio("happy");

        //Munition übergeben
        LevelManager.instance.ammo += ammoCarriedByTeddy;
        ammoCarriedByTeddy = 0;

        //Timer auf Phase 2 resetten
        if(LevelManager.instance.safety < LevelManager.instance.phaseTwoThreshold)
        {
            LevelManager.instance.safety = LevelManager.instance.phaseTwoThreshold + 0.1f;
            LevelManager.instance.ResetPhase2();
            AudioGameModell.instance.StopAudio("TeddySuffering");
            sufferingAudioPlaying = false;
        }

        //Sicherer Status für Teddy
        safe = true;

    }

    //Monster abwehren
    public void ActivateLight()
    {
        LevelManager.instance.RepelMonster(safe);
    }

    //private void OnDestroy()
    //{
    //    GameManager.instance.hp = hp;
    //}
}
