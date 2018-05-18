using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour {

    public Sprite box2, box3, box4, key;
    public GameObject doorToggle;

    SpriteRenderer spriteRenderer;
    int stage = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void KickBox()
    {
        switch (stage)
        {
            case 0:
                spriteRenderer.sprite = box2;
                AudioModell.instance.PlayAudio("Kick");
                stage++;
                break;
            case 1:
                spriteRenderer.sprite = box3;
                AudioModell.instance.PlayAudio("Kick");
                stage++;
                break;
            case 2:
                spriteRenderer.sprite = box4;
                AudioModell.instance.PlayAudio("Kick");
                stage++;
                break;
            case 3:
                spriteRenderer.sprite = key;
                AudioModell.instance.PlayAudio("Kick");
                stage++;
                break;
            case 4:
                spriteRenderer.sprite = null;
                AudioModell.instance.PlayAudio("Key");
                doorToggle.SetActive(true);
                stage++;
                break;
        }
    }
}
