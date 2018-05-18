using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UberManager : MonoBehaviour {

    public int matches = 0;
    public static UberManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
