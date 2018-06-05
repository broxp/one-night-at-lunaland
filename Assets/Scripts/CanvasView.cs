using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasView : MonoBehaviour {

    public Text statusText;
    public double hp, ammoCarriedByTeddy, ammo, safety;
    public static CanvasView instance = null;
    public GameObject PickUpItemBubble, LeaveItemBubble, UpBubble, DownBubble, matchUI, happyBubblePrefab, unhappyBubblePrefab;
    public Sprite[] matchSprites;

    GameObject moodBubble;
    Image matchImage;
    Transform tikUI1, tikUI2;
    Camera cam;

    private void Update()
    {
        UpdateStatusText();

        if(moodBubble != null)
        {
            moodBubble.transform.position = cam.WorldToScreenPoint(tikUI2.position);
        }
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
        matchImage = matchUI.GetComponent<Image>();
        tikUI1 = GameObject.FindGameObjectWithTag("UI1").transform;
        tikUI2 = GameObject.FindGameObjectWithTag("UI2").transform;
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

    //Streichhölzer Zahl anpassen
    public void UpdateMatchCount(int _amount)
    {
        if(_amount < matchSprites.Length && _amount >= 0)
        {
            matchImage.sprite = matchSprites[_amount];
        }
    }

    //UI für Item-Pickup
    public void ShowCrossingBubble()
    {
        if (moodBubble != null)
        {
            Destroy(moodBubble);
        }

        GameObject upBubble = GameObject.Instantiate(UpBubble, transform);
        GameObject downBubble = GameObject.Instantiate(DownBubble, transform);


        upBubble.transform.position = cam.WorldToScreenPoint(tikUI1.position);
        downBubble.transform.position = cam.WorldToScreenPoint(tikUI2.position);
    }

    public IEnumerator ShowHappyBubble()
    {
        if(moodBubble != null)
        {
            Destroy(moodBubble);
        }
        moodBubble = GameObject.Instantiate(happyBubblePrefab, cam.WorldToScreenPoint(tikUI2.position), Quaternion.identity, transform);
        yield return new WaitForSeconds(4);
        Destroy(moodBubble);
    }

    public IEnumerator ShowUnhappyBubble()
    {
        if (moodBubble != null)
        {
            Destroy(moodBubble);
        }
        moodBubble = GameObject.Instantiate(unhappyBubblePrefab, cam.WorldToScreenPoint(tikUI2.position), Quaternion.identity, transform);
        yield return new WaitForSeconds(4);
        Destroy(moodBubble);
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
