using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHands : MonoBehaviour
{
    HandManager handManager;

    // Start is called before the first frame update
    void Start()
    {
        HandManager.singleton = handManager;

    }

    public void DistanceHand()
    {
        HandManager.singleton.EnableDistanceHands();
        // handManager.EnableDistanceHands();
    }

    public void CustomHand()
    {

        handManager.EnableCustomHands();
    }

    public void ManualHand()
    {
        handManager.EnableManualHands();
    }



}
