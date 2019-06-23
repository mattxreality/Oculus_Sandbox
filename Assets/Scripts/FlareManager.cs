using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareManager : MonoBehaviour
{
    // Play flare ignite when we hit an object with a big velocity

    // todo possibly apply this script to parent flare go

    /* todo use relativeVelocity to ignite flare
     *
     * DONE Create impact object at bottom of flare
     * DONE make child of flare
     * DONE add ignite script to impact go
     * DONE (0.2) Set rb mass lower so the flare can be thrown farther
     * DONE Add lighting effect to flare
     * Use light lerp code from Rocket2
     * Add duration script so flare light and PS last same time
     *      Use countDown -= Time.deltaTime; // confirm with whiteboard
     * 
     * make ignite script enable flare particleSystem (use SendMessage)
     * 
     * Before strike surface is hit, glow yellow/orange
     * When strike surface is hit, change color to gray ('used')
     * 
     * make two light sources, one red bright, other yellow dimmer. Oscallate at diff intervals.
     */

    // Interpolate light color between two colors back and forth

    private OVRGrabbable ovrGrabbable;

    [SerializeField] Light lightSource;
    [SerializeField] float lightColorOscillationRate = 1.0f;
    [SerializeField] Color lightColor01;
    [SerializeField] Color lightColor02;

    [SerializeField] ParticleSystem igniteParticle;
    [SerializeField] ParticleSystem flareParticle;

    [SerializeField] float coolDownValue = 15f;
    private float currCoolDownValue; // used for limited duration flare

    public OVRInput.Button onButton;
    public OVRInput.Button offButton;

    public AudioSource flareAudio;

    void Start()
    {
        flareAudio = GetComponent<AudioSource>();
        flareAudio.Stop();
        ovrGrabbable = GetComponent<OVRGrabbable>();
        lightSource.enabled = !lightSource.enabled; // start with light off
        igniteParticle.Stop();
        flareParticle.Stop();

        currCoolDownValue = coolDownValue;
    }

    void Update()
    {
        SetLightColor();

        if (currCoolDownValue == 0)
        {
            // turn off flare
            ExtinguishFlare();
        }

        DebugMyStuff();
    }

    void CountDown()
    {
        print("Countdown initiated");
        StartCoroutine(CountDownTimer(coolDownValue));
    }

    private void DebugMyStuff()
    {
        // todo turn on/off when grabbed. Starter code below...
        // OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)
        if (ovrGrabbable.isGrabbed && OVRInput.Get(onButton, ovrGrabbable.grabbedBy.GetController()))
        {
            // make controller vibrate using manual frequency parameters
            VibrationManager.singleton.TriggerVibrationManual(40, 2, 255, ovrGrabbable.grabbedBy.GetController());
            LightFlare();
            print("light source status" + lightSource.enabled.ToString());
        }

        if (ovrGrabbable.isGrabbed && OVRInput.Get(offButton, ovrGrabbable.grabbedBy.GetController()))
        {
            ExtinguishFlare();
        }
    }

    private void LightFlare()
    {
        igniteParticle.Play();
        flareParticle.Play();
        lightSource.enabled = true;
        flareAudio.Play();

    }

    private void ExtinguishFlare()
    {
        igniteParticle.Stop();
        flareParticle.Stop();
        lightSource.enabled = false;
        flareAudio.Stop();
    }

    private void SetLightColor()
    {
        // set light color
        float t = Mathf.PingPong(Time.time, lightColorOscillationRate) / lightColorOscillationRate;
        lightSource.color = Color.Lerp(lightColor01, lightColor02, t);
    }

    private IEnumerator CountDownTimer(float coolDownValue)
    {
        // counts down one second for each unit of coolDownValue
        currCoolDownValue = coolDownValue;
        while (currCoolDownValue > 0)
        {
            // Debug.Log("Countdown: " + currCoolDownValue);
            yield return new WaitForSeconds(1.0f);
            currCoolDownValue--;
        }
    }
}
