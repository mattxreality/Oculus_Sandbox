using UnityEngine;


    // A script that prevents a gameObject from destroying itself between scenes

    public class DontDestroyObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }


