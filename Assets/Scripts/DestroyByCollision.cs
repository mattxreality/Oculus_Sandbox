using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) // detects collider contact of GameObject
    {
        Destroy(this.gameObject);
        //print("Object triggered something");
        //print("other game object = " + collision.gameObject.name);
    }

    //void OnTriggerEnter(Collider other){ }

}
