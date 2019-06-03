using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] float lifetime = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
