using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeddyModell : MonoBehaviour {

    public int ammoCarriedByTeddy;
    public float hp, movementForce;
    public bool safe;

    Rigidbody2D rigidbody;

    private void Start()
    {
        safe = false;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateCanvasView();

        if (GameLogic.instance.safety <= 0)
        {
            if(safe || hp <= 0)
            {
                GameLogic.instance.LoadLevel();
            }
            else
            {
                hp -= GameLogic.instance.monsterDmg * Time.deltaTime;
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
            GameLogic.instance.gamePaused = true;
        }

        if(collision.gameObject.tag == "UpRamp")
        {
            GameLogic.instance.DeactivateUpWays();
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
        GameLogic.instance.gamePaused = false;
        GameLogic.instance.ActivateUpWays();
        CanvasView.instance.ClearBubbles();
        //AudioModell.instance.PlayAudio("suffering");
    }

    //Nach unten gehen
    public void GoDown()
    {
        print("down!");
        GameLogic.instance.gamePaused = false;
        GameLogic.instance.DeactivateUpWays();
        CanvasView.instance.ClearBubbles();
    }

    //Item aufheben
    public void PickItemUp(GameObject item)
    {
        print("picked up!");
        GameObject.Destroy(item);
        ammoCarriedByTeddy++;
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

        //AudioModell.instance.PlayAudio("happy");

        //Munition übergeben
        GameLogic.instance.ammo += ammoCarriedByTeddy;
        ammoCarriedByTeddy = 0;

        //Timer auf Phase 2 resetten
        if(GameLogic.instance.safety < GameLogic.instance.phaseTwoThreshold)
        {
            GameLogic.instance.safety = GameLogic.instance.phaseTwoThreshold;
        }

        //Sicherer Status für Teddy
        safe = true;

    }

    //Monster abwehren
    public void ActivateLight()
    {
        GameLogic.instance.RepelMonster();
    }

    private void OnDestroy()
    {
        UberManager.instance.hp = hp;
    }
}
