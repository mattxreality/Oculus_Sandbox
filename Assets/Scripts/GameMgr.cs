using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr instance = null;

    Scene scene;
    Animator anim;
    Image fadeImage;

    [Header("Scene Management")]
    public string[] scenesToLoad;
    public string activeScene;

    [Space]
    [Header("UI Settings")]
    public bool useFade;
    public GameObject fadeOverlay;
    public GameObject ControlUI;
    public GameObject LoadingUI;

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

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        activeScene = scene.buildIndex + " - " + scene.name;
    }

    //Select scene is called from either the menu manager or hotspot manager, and is used to load the desired scene
    public void SelectScene(string sceneToLoad)
    {
        //if we want to use the fading between scenes, start the coroutine here
        if (useFade)
        {
            StartCoroutine(FadeOutAndIn(sceneToLoad));
        }
        //if we dont want to use fading, just load the next scene
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        //set the active scene to the next scene
        activeScene = sceneToLoad;
    }

    IEnumerator FadeOutAndIn(string sceneToLoad)
    {
        //get references to animatior and image component 
        anim = fadeOverlay.GetComponent<Animator>();
        fadeImage = fadeOverlay.GetComponent<Image>();

        //set FadeOut to true on the animator so our image will fade out
        anim.SetBool("FadeOut", true);

        //wait until the fade image is entirely black (alpha=1) then load next scene
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        SceneManager.LoadScene(sceneToLoad);
        Scene scene = SceneManager.GetSceneByName(sceneToLoad);
        Debug.Log("loading scene:" + scene.name);
        yield return new WaitUntil(() => scene.isLoaded);

        //set FadeOUt to false on the animator so our image will fade back in 
        anim.SetBool("FadeOut", false);

        //wait until the fade image is completely transparent (alpha = 0) and then turn loading UI off and control UI back on
        yield return new WaitUntil(() => fadeImage.color.a == 0);         
    }
}
