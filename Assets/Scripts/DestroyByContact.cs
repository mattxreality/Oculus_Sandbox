using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public ParticleSystem ripples;
    public int scoreValue;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FX" | other.tag == "light" | other.tag == "gate" | other.tag == "accelerator") { return; }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        /*
         * print("Object triggered something");
         * print("other game object = " + other.gameObject.name);
         * print("other game object tag = " + other.gameObject.tag);
        */

        if(ripples != null && other.tag == "water")
        {
            Instantiate(ripples, transform.position +Vector3.up, Quaternion.Euler(-90,0,0));
        }
    Destroy(gameObject);

    }
}
