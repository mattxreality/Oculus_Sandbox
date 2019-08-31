using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



    public class SceneSwitchOnEnable : MonoBehaviour
    {
        public int sceneToSwitchTo;

        private bool firstTime;

        private void OnEnable()
        {
            if (!firstTime)
            {
                print("switching to scene: " + NameFromIndex(sceneToSwitchTo));

                if (SceneSwitcher.Instance != null)
                    SceneSwitcher.Instance.SwitchScene(sceneToSwitchTo);
                else
                {
                    SceneManager.LoadScene(sceneToSwitchTo, LoadSceneMode.Single);
                }

                firstTime = true;
            }
            else
            {
                firstTime = false;
                this.enabled = false;
            }          
        }
        
        private static string NameFromIndex(int BuildIndex)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
            int slash = path.LastIndexOf('/');
            string name = path.Substring(slash + 1);
            int dot = name.LastIndexOf('.');
            return name.Substring(0, dot);
        }

    }



