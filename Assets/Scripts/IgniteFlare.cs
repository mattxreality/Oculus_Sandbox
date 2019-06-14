using UnityEngine;
using System.Collections;


public class IgniteFlare : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > .1)
        {
            print("Collision RelativeVelocity.magnitude = " + collision.relativeVelocity.magnitude.ToString());
            SendMessage("LightFlare");
            // todo send message to start flare

            SendMessage("CountDown"); // countdown to turn off flare
        }
    }
}
