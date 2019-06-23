using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager singleton;

    [SerializeField] GameObject[] distanceGrabHands;
    [SerializeField] GameObject[] customGrabHands;
    [SerializeField] GameObject[] manualGrabHands;

    enum State { distanceHands, customHands, manualHands };
    State currentState = State.manualHands;

    /*
     * Create state machine
     * Make methods public
     * for each state enable/disable correct GOs in scene
     * make UI buttons change state
     */

    // Start is called before the first frame update
    void Start()
    {
        // make this class a singleton
        if (singleton && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(currentState == State.distanceHands)
        {
            foreach(GameObject hand in distanceGrabHands)
            {
                if (hand.activeSelf)
                {
                    Debug.Log("State " + currentState);
                    return;
                }
            }

            Debug.Log("State changing to " +currentState);

            //Enable Distance Hands, Disable others
            foreach (GameObject hand in distanceGrabHands)
                {
                    hand.SetActive(true);
                Debug.Log("distanceGrabHands Enabled");
                }

            foreach (GameObject hand in customGrabHands)
                {
                    hand.SetActive(false);
                Debug.Log("customGrabHands Disabled");
            }
            foreach (GameObject hand in manualGrabHands)
            {
                OVRGrabber myScript;

                myScript = hand.GetComponent<OVRGrabber>();

                if (myScript.enabled)
                {
                    myScript.enabled = !myScript.enabled;
                    Debug.Log("manualGrabHands Disabled");
                }
                
            }

        }
        if (currentState == State.customHands)
        {
            // do something
        }
        if (currentState == State.manualHands)
        {
            // do something
        }
    }

    public void EnableDistanceHands()
    {
        currentState = State.distanceHands;
    }

    public void EnableCustomHands()
    {
        currentState = State.customHands;
    }

    public void EnableManualHands()
    {
        currentState = State.manualHands;
    }

}
