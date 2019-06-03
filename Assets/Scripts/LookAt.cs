using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    
    void Start()
    {
        
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(transform.position * 2 - target.position);
    }
}
