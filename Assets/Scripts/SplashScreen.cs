using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float waitTime = 5.0f;

    private int loadingSceneIndex = 1;

    //wait x seconds then load MainMenu
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(loadingSceneIndex);
    }
}
