using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // todo must normalize projectile speed, currently too slow at beginning, to fast later

    [SerializeField] float speed = 50f;
    [SerializeField] float speedIncrease = 1f;
    [SerializeField] float speedNormalizer = .7f;
    //Rigidbody rigidbody;

    void Start()
    {
        // Start() is sufficient as this script controls projectiles that only
        // live for short periods of time. Each projectile will get the latest
        // speed info from SpeedControl().
        //speedIncrease = SpeedControl.instance.speedIncrease; // external reference

        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
