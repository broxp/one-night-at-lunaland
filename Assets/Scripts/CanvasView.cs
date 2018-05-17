using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasView : MonoBehaviour {

    public Text statusText;
    public double hp, ammoCarriedByTeddy, ammo, safety;
    public static CanvasView instance = null;
    public GameObject PickUpItemBubble, LeaveItemBubble, UpBubble, DownBubble;

    Transform tikUI1, tikUI2;
    Camera cam;

    private void Update()
    {
        UpdateStatusText();
    }

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

    //Get Camera Reference
    private void Start()
    {
        tikUI1 = GameObject.FindGameObjectWithTag("UI1").transform;
        tikUI1 = GameObject.FindGameObjectWithTag("UI2").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    //UI für Wegekreuzung
    public GameObject ShowItemBubble()
    {
        GameObject pickUpBubble = GameObject.Instantiate(PickUpItemBubble, transform);
        GameObject leaveItemBubble = GameObject.Instantiate(LeaveItemBubble, transform);

        pickUpBubble.transform.position += new Vector3(200, 0, 0);
        leaveItemBubble.transform.position += new Vector3(-200, 0, 0);

        return pickUpBubble;
    }

    //UI für Item-Pickup
    public void ShowCrossingBubble()
    {
        GameObject upBubble = GameObject.Instantiate(UpBubble, transform);
        GameObject downBubble = GameObject.Instantiate(DownBubble, transform);

        upBubble.transform.position = cam.WorldToScreenPoint(tikUI1.position);
        downBubble.transform.position = cam.WorldToScreenPoint(tikUI2.position);
    }

    //Löscht alle UI-Elemente mit dem "Bubble"-Tag (also alle Sprechblasen)
    public void ClearBubbles()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.tag == "Bubble")
            {
                GameObject.Destroy(child.gameObject);
            }            
        }
    }

    //Status-Text Updaten
    void UpdateStatusText()
    {
        statusText.text = "HP: " + hp + "\n"
        + "ammoCarriedByTeddy: " + ammoCarriedByTeddy + "\n"
        + "Ammo: " + ammo + "\n"
        + "Safety: " + safety + "\n";
    }

}
