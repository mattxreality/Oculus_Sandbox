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


    void Awake()
    {
        
        // check if this is the only instance. If not, destroy this instance.
        #region SINGLETON
        if(singleton && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }

        DontDestroyOnLoad(gameObject);
        #endregion

    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSandbox01()
    {
        // todo control fade on OVRScreenFade
        centralEyeAnchor.GetComponent<OVRScreenFade>().FadeOut();
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (fadeComplete)
        {
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            fadeComplete = false;
            SceneManager.LoadScene(nextLevel);
        }

        // old scene change triggered by controller
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= Mathf.Epsilon)
            {
                // todo OVR fade before changing level
                //LevelChanger.instance.FadeToLevel(1);
                //Invoke("LoadFirstScene", 3f);
            }
        }


    }

}
