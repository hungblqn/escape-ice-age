using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float timeStart;
    public float fireRate;
    

    void Start()
    {
        InvokeRepeating("Shoot", timeStart, fireRate);
    }

    void Shoot()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
