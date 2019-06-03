using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAtRuntime : MonoBehaviour
{
    private bool state = false;

    void Start()
    {
        gameObject.SetActive(state);
    }
}
