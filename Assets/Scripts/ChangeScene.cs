using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    // do I even need this?
    //SceneLoader sceneLoader;
    
    // Start is called before the first frame update
    void Start()
    {
        // do I even need this?
        //SceneLoader.singleton = sceneLoader;
    }


    public void LoadTruckRepair()
    {
        Debug.Log("Button Press for LoadTruckRepair");
        SceneLoader.singleton.LoadTruckRepair();
        //sceneLoader.LoadTruckRepair();

    }

    public void LoadDesert()
    {
        Debug.Log("Button Press for LoadDesert");
        SceneLoader.singleton.LoadDesert();

    }

    public void LoadSandbox01()
    {
        Debug.Log("Button Press for LoadSandbox01");
        SceneLoader.singleton.LoadSandbox01();
        //sceneLoader.LoadSandbox01();
    }

    public void LoadStartMenu()
    {
        Debug.Log("Button Press for LoadStartMenu");
        SceneLoader.singleton.LoadStartMenu();
    }

}
