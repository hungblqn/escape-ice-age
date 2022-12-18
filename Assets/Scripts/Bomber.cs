using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public int maxHealth = 1000;
    public int health;

    Rigidbody bomberRigidbody;
    public GameObject player;
    public GameObject middleEye;
    public GameObject bullet;
    public GameObject rageBullet;
    public HealthBar healthBar;

    private PlayerController playerController;
    private Vector3 lookPosition;
    private bool rage = false;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        bomberRigidbody = GetComponent<Rigidbody>();
        StartCoroutine("Shoot",3);
        healthBar.SetMaxHealth(maxHealth);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(player!= null)
        {
            transform.LookAt(player.transform.position);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        healthBar.SetHealth(health);
        if (health < maxHealth / 2) rage = true;
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Bullet"))
        {
            health -= 10;
            Destroy(other.gameObject);
        }
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            if(rage == false)
            {
                yield return new WaitForSeconds(2);
                Instantiate(bullet, middleEye.transform.position, transform.rotation);
            }
            if(rage == true)
            {
                yield return new WaitForSeconds(1.7f);
                Instantiate(rageBullet, middleEye.transform.position - new Vector3(20, 0, 0), transform.rotation);
            }
        }
    }
    private void OnDestroy()
    {
        playerController.key++;
    }
}
