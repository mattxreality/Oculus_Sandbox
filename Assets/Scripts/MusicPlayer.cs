using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Makes this class a singleton
    public static MusicPlayer singleton;

    void Awake()
    {
        // check if this is the only instance. If not, destroy this instance. Creates a public class.

        if (singleton && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
            
        }
        //DontDestroyOnLoad(gameObject);
    }
}
