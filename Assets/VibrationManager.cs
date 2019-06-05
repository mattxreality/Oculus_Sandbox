using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour {

    // make this class a singleton
    public static VibrationManager singleton;
    
    void Start()
    {
        // make this class a singleton
        if (singleton && singleton != this)
            Destroy(this);
        else
            singleton = this; 
    }

// trigger haptic vibration from audio clip
public void TriggerVibration(AudioClip vibrationAudio, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip(vibrationAudio);

        if(controller == OVRInput.Controller.LTouch)
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
