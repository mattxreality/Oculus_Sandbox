using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    // maybe for me to get the grenade trigger to work I should just use when the colliders intersect.
    // if they're touching, the trigger can activate the grenade.

    public float delay = 3f;

    public GameObject explosionEffect;
    public OVRGrabbable ovrGrabbable;
    public float blastRadius = 7f;
    public float force = 700f;

    GameObject grabbableScript;

    float countdown;
    bool hasExploded = false;

    bool isGrabbed = false;

    private void Awake()
    {
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    void Start()
    {
        countdown = delay;
        
    }

    void Update()
    {
        if(ovrGrabbable.isGrabbed)
        {
            Debug.Log("Grenade is grabbed");
            isGrabbed = true;
        }
        else
        {
            isGrabbed = false;
        }

        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Debug.Log("Boom!");
            Explode();
            hasExploded = true;
        }

        if (isGrabbed)
        {
            Debug.Log("isGrabbed = true");
        }

        //Oculus Touch Triggers and general Fire1
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= Mathf.Epsilon)
        {
            // todo && isGrabbed, then trigger timer
            Debug.Log("left index trigger pulled");
        }

    }

    void Explode()
    {
        // todo add to destructible script
        // todo separate colliders to destroy from to move

        // show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // get all nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        // get rb of each nearby object and apply force
        foreach (Collider nearbyObject in colliders)
        {
            
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb !=null)
            {
                rb.AddExplosionForce(force, transform.position, force);
            }
        }

        // destroy self
        Destroy(gameObject);

    }
}
