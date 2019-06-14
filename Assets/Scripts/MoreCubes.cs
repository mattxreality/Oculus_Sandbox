using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCubes : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform spawnPoint;

    public void Cube()
    {
        Instantiate(cubePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

}
