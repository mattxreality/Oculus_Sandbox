using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


    // A script in charge of switching scenes

    public class SceneSwitcher : MonoBehaviour
    {

        #region EVENTS
        public static event Action SceneLoadedCallback;

        protected virtual void OnSceneLoaded()
        {
            SceneLoadedCallback?.Invoke();
        }
        #endregion

        public static SceneSwitcher Instance;

        public bool useFade;

        [HideInInspector]
        public bool isChangingScene;

        [HideInInspector]
        public bool isLevelLoaded;

        private FadeTransition fadeTransition;
        private AsyncOperation asyncLoadLevel;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            if (FindObjectOfType<FadeTransition>() == null)
            {
                Debug.LogError("There is no FadeTransition object in the scene!");
            }
            else
            {
                fadeTransition = FindObjectOfType<FadeTransition>();
            }
        }

        public void SwitchScene(int sceneToLoad)
        {
            if (!isChangingScene)
                StartCoroutine(SceneTransition(sceneToLoad));            
        }
        
        private IEnumerator SceneTransition(int sceneToLoad)
        {
            isChangingScene = true;
            isLevelLoaded = false;

            // Fade Out
            fadeTransition.LoadScene(FadeTransition.FadeType.FadeOut);
            yield return new WaitUntil(() => fadeTransition.fadeComplete);

            // Loads Scene
            asyncLoadLevel = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
            yield return new WaitUntil(() => asyncLoadLevel.isDone);

            Debug.Log("Next scene loaded");

            OnSceneLoaded();

            isLevelLoaded = true;

            var videoPlayer = FindObjectOfType<VideoPlayer>();
            if (videoPlayer != null)
            {
                yield return new WaitForSeconds(3F);
                //yield return new WaitUntil(() => videoPlayer.isPrepared);
            }
            else
                Debug.Log("There's no video in the scene");

            Debug.Log("Video buffer time ended");
            //Debug.Log("Video Loaded");

            // Fades In
            fadeTransition.LoadScene(FadeTransition.FadeType.FadeIn);
            yield return new WaitUntil(() => fadeTransition.fadeComplete);

            isChangingScene = false;
        }
    }
