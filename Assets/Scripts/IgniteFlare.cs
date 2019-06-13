using UnityEngine;
using System.Collections;


public class IgniteFlare : MonoBehaviour
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
    [SerializeField] Light lightSource;
    [SerializeField] float lightColorOscillationRate = 1.0f;
    [SerializeField] Color lightColor01;
    [SerializeField] Color lightColor02;

    [SerializeField] ParticleSystem igniteSparks;
    [SerializeField] ParticleSystem flareFlame;

    [SerializeField] float coolDownValue = 15f;
    private float currCoolDownValue; // used for countdown and resetting lights & collision

    void Start()
    {
 
    }

    void Update()
    {
        if (currCoolDownValue == 0)
        {
            // turn off flare
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            // todo send message to start flare
            StartCoroutine(CountDownTimer(coolDownValue)); // countdown to turn off flare
        }
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
