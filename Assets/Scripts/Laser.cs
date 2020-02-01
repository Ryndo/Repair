using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject firePoint;
    GameObject spawnedLaser;

    void Start(){
        spawnedLaser = Instantiate(laserPrefab,firePoint.transform) as GameObject;
    }
}
