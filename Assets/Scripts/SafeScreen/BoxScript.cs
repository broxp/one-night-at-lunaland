using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour {

    public Sprite box2, box3, box4;
    public GameObject doorToggle, exitToggle, key;

    SpriteRenderer spriteRenderer;
    int stage = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void KickBox(SafeScreenModell _safeScreenModell)
    {
        switch (stage)
        {
            case 0:
                spriteRenderer.sprite = box2;
                AudioModell.instance.PlayAudio("kick");
                AudioModell.instance.PlayAudio("noboxkickingpls");
                stage++;
                break;
            case 1:
                spriteRenderer.sprite = box3;
                AudioModell.instance.PlayAudio("kick");
                stage++;
                break;
            case 2:
                spriteRenderer.sprite = box4;
                AudioModell.instance.PlayAudio("kick");
                stage++;
                break;
            case 3:
                AudioModell.instance.PlayAudio("kick");
                key.SetActive(true);
                Destroy(this.gameObject);

                stage++;
                break;
        }
    }
}
