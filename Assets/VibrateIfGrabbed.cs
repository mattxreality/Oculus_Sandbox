using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateIfGrabbed : MonoBehaviour
{
    private OVRGrabbable ovrGrabbable;
    // Start is called before the first frame update
    void Start()
    {
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if object is grabbed and if touch controller button is pressed
        if (ovrGrabbable.isGrabbed)// && OVRInput.GetDown(shootingButton, ovrGrabbable.grabbedBy.GetController()))
        {
            // make controller vibrate using an audio file
            //VibrationManager.singleton.TriggerVibration(shootingAudio,ovrGrabbable.grabbedBy.GetController());

            // make controller vibrate using manual frequency parameters
            VibrationManager.singleton.TriggerVibrationManual(40, 2, 255, ovrGrabbable.grabbedBy.GetController());
        }
    }
}
