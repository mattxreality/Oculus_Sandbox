using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    // do I even need this?
    SceneLoader sceneLoader;
    
    // Start is called before the first frame update
    void Start()
    {
        // do I even need this?
        sceneLoader.singleton = sceneLoader;
    }


    public void LoadSandbox01()
    {
        // stuff SceneLoader.singleton.LoadSandbox01();
    }

    public void LoadStartMenu()
    {
        // stuff SceneLoader.singleton.LoadStartMenu();
    }

}
