using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    // A script used to fade in and out between scenes

    public class FadeTransition : MonoBehaviour
    {
        public static FadeTransition Instance;
       
        public Image fadeImage;
        public float fadePredelay;
        public float fadeLength;
        
        [HideInInspector]
        public bool fadeComplete;

        public enum FadeType
        {
            FadeIn,
            FadeOut
        }

        private bool fadeIn;
        private bool fadeOut;
        private float fadePredelayCounter;

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
        }

        private void Start()
        {
            LoadScene(FadeType.FadeIn);
        }

        private void Update()
        {
            if (fadeIn && !fadeOut && !fadeComplete)
                FadeInNew();

            if (fadeOut && !fadeIn && !fadeComplete)
                FadeOutNew();
        }

        public void LoadScene(FadeType fadeType)
        {
            fadeComplete = false;
            fadePredelayCounter = fadePredelay;

            ResetFadeValues();


            switch (fadeType)
            {
                case FadeType.FadeIn:                   
                    fadeIn = true;
                    break;

                case FadeType.FadeOut:
                    fadeOut = true;
                    break;
            }
        }
            
        private void FadeInNew()
        {
            FadePreDelay();

            if (fadePredelayCounter <= 0)
            {
                if (fadeImage.color.a > 0)
                {
                    Color tempColor = fadeImage.color;

                    tempColor.a -= Time.deltaTime * (1 / fadeLength);

                    fadeImage.color = tempColor;

                    if (fadeImage.color.a <= 0)
                    {
                        fadeComplete = true;
                        ResetFadeValues();
                    }
                }                
            }           
        }

        private void FadeOutNew()
        {
            FadePreDelay();

            if (fadePredelayCounter <= 0)
            {
                if (fadeImage.color.a < 1)
                {
                    Color tempColor = fadeImage.color;

                    tempColor.a += Time.deltaTime * (1 / fadeLength);

                    fadeImage.color = tempColor;

                    if (fadeImage.color.a >= 1)
                    {
                        fadeComplete = true;
                        ResetFadeValues();
                    }
                }
            }           
        }

        private void FadePreDelay()
        {
            if (fadePredelayCounter > 0)
            {
                fadePredelayCounter -= Time.deltaTime;
            }
        }

        private void ResetFadeValues()
        {
            fadeIn = false;
            fadeOut = false;
        }
    }



