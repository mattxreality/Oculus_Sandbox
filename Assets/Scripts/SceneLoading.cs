using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public static SceneLoading singleton;

    [SerializeField]
    private Image progressBar;
    public int sceneIndexToLoad = 2;

    private void Awake()
    {
        if (singleton && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
        // DontDestroyOnLoad(gameObject);

        // sceneIndexToLoad = 2; // first scene to load is mainmenu
    }

    void Start()
    {
        // start async operation
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {

        yield return new WaitForSeconds(3f);

        //create an async operation
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneIndexToLoad);
        
            while (gameLevel.progress < 1)
        {
            // The progress bar fill = async operation progress
            progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame(); // let program breath
        }

        // when finished, load the game scene
        yield return new WaitForEndOfFrame(); // let program breath
    }
}
