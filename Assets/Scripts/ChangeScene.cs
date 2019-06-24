using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    // do I even need this?
    // SceneLoader sceneLoader;
    
    // Start is called before the first frame update
    void Start()
    {
        // do I even need this?
        // SceneLoader.singleton = sceneLoader;
    }


    public void LoadSandbox01()
    {
        SceneLoader.singleton.LoadSandbox01();
        Debug.Log("Button Press for LoadSandbox01");
    }

    public void LoadStartMenu()
    {
        SceneLoader.singleton.LoadStartMenu();
    }

}
