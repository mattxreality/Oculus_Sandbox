using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] GameObject centralEyeAnchor;

    // Makes this class a singleton
    public static SceneLoader singleton;

    [Tooltip("In seconds")] [SerializeField] float loadDelay = 5f;
    public bool fadeComplete = false;

    int totalScenes;
    int nextSceneIndex;
    int currentScene;  // create an integer variable for storing the current scene index
    float fadeTime;

    void Awake()
    {

        // check if this is the only instance. If not, destroy this instance.

        // todo - having trouble with singletons when I return to the scene they were created

        if (singleton && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
       // DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        totalScenes = SceneManager.sceneCountInBuildSettings - 1; // # scenes in build settings
        currentScene = SceneManager.GetActiveScene().buildIndex;  // get the active scene's index, stored as int
        fadeTime = centralEyeAnchor.GetComponent<OVRScreenFade>().fadeTime;
    }

    private void SceneTransition()
    {
        if (currentScene != nextSceneIndex)
        {
            centralEyeAnchor.GetComponent<OVRScreenFade>().FadeOut();
            StartCoroutine(LoadScene(fadeTime)); // load scene after fadeTime completes
            currentScene = nextSceneIndex;
            Debug.Log("running StartMenu()");
        }
    }

    public void LoadStartMenu()
    {
        nextSceneIndex = 0; // scene index destination

        SceneTransition();
    }

    public void LoadSandbox01()
    {
        nextSceneIndex = 1; // scene index destination

        SceneTransition();
    }

    IEnumerator LoadScene(float delay = 0.0f) // Coroutine with built-in delay
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        // The rest of your coroutine here
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void Update()
    {

        if (Input.GetKeyDown("l"))
        {
            // fade out
            centralEyeAnchor.GetComponent<OVRScreenFade>().FadeOut();
            Invoke("LoadSandbox01", centralEyeAnchor.GetComponent<OVRScreenFade>().fadeTime);
            Debug.Log("FadeOut initiated. 'l' pressed");
        }
        if (Input.GetKeyDown("k"))
        {
            // fade in
            centralEyeAnchor.GetComponent<OVRScreenFade>().FadeIn();
            Invoke("LoadSandbox01", centralEyeAnchor.GetComponent<OVRScreenFade>().fadeTime);
            Debug.Log("FadeOut initiated. 'k' pressed");
        }

    }

    // todo - create async scene loading
    // source - https://forum.unity.com/threads/loading-new-scene-in-background-vr.436025/

    //IEnumerator loadSceneAsync()
    //{
    //    AsyncOperation async = SceneManager.LoadAsyncScene("Scene", LoadSceneMode.Single);
    //    async.allowSceneActivation = false;
    //    while (async.progress < 0.9f)
    //    {
    //        progressText.text = async.progress + "";
    //    }
    //    async.allowSceneActivation = true;
    //    while (!async.isDone)
    //    {
    //        yield return null;
    //    }
    //}

}
