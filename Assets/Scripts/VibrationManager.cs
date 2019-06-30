using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*   INSTRUCTIONS ON HAPTIC USAGE
 *   
 *   1. Add either of the below to an action that triggers haptics. 
 *   These are usually placed on an action script (like shooting control).
 *   (e.g. button click, grab, audio source, proximity, etc)
 *   2. Remember that haptics can clips can be
 *      Mix - Blend two or more clips together
 *      Preempt - Stop current clip and start new clip
 *      Queue - Wait for current clip then run new clip
 *   
 *   make controller vibrate using an audio file
 *   VibrationManager.singleton.TriggerVibration(shootingAudio,ovrGrabbable.grabbedBy.GetController());
 *
 *   make controller vibrate using manual frequency parameters
 *   VibrationManager.singleton.TriggerVibrationManual(40,2,255, ovrGrabbable.grabbedBy.GetController());
*/


public class VibrationManager : MonoBehaviour {

    // make this class a singleton
    public static VibrationManager singleton;

    void Awake()
    {
        // check if this is the only instance. If not, destroy this instance.
        if (singleton && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
            
        }
        DontDestroyOnLoad(gameObject);
    }

// trigger haptic vibration from audio clip
public void TriggerVibration(AudioClip vibrationAudio, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip(vibrationAudio);

        if(controller == OVRInput.Controller.LTouch)
        {
            // trigger on Left controller
            OVRHaptics.LeftChannel. Preempt(clip);
        }

        else if (controller == OVRInput.Controller.RTouch)
        {
            // trigger on Right controller
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }

// trigger haptic vibration from array of numbers
    public void TriggerVibrationManual(int iteration, int frequency, int strength, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip();

        for (int i = 0; i < iteration; i++)
        {
            clip.WriteSample(i % frequency == 0 ? (byte)strength : (byte)0);
        }

        if (controller == OVRInput.Controller.LTouch)
        {
            // trigger on Left controller
            OVRHaptics.LeftChannel.Preempt(clip);
        }

        else if (controller == OVRInput.Controller.RTouch)
        {
            // trigger on Right controller
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }
}
