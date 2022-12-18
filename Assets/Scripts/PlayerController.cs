using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float health = 10;
    public float movementSpeed = 3;
    public float slidingSpeed = 5;
    public float rotateSpeed = 180;
    public float jumpForce = 200f;
    public float fireRate;


    public float timeScale;

    public int key;

    public Text healthText;
    public Text restartText;

    public LayerMask ground, ice,ice2;
    public GameObject groundCheck;

    public GameObject bullet;

    public Camera spectator;
    public AudioSource dieSound;

    public GameObject door;

    public Animator anim;


    private float timeStamp;
    private Vector3 checkPoint;

    private bool rotateDoor = false;
    private Vector3 test;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        spectator.enabled = false;
        restartText.enabled = false;
        if (door != null)
        {
            test = new Vector3(0, 90 + door.transform.eulerAngles.y, 0);
        }
        //checkPoint = transform.position;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
        healthText.text = "Health: " + health +"\nKey: "+key;
        
        if (isGrounded())
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerRb.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else anim.SetBool("isRunning", false);
            if (Input.GetKey(KeyCode.S))
            {
                playerRb.transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);
                anim.SetBool("isRunningBack", true);
            }
            else anim.SetBool("isRunningBack", false);
            if (Input.GetKeyDown(KeyCode.Space)){
                playerRb.AddForce(Vector3.up * jumpForce);
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerRb.transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerRb.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerRb.transform.Rotate(-Vector3.up * rotateSpeed / 3 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerRb.transform.Rotate(Vector3.up * rotateSpeed / 3 * Time.deltaTime);
            }
        }
        if (isSliding())
        {
            playerRb.transform.Translate(Vector3.forward * slidingSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.A))
            {
                playerRb.transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerRb.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerRb.transform.Rotate(-Vector3.up * rotateSpeed / 3 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerRb.transform.Rotate(Vector3.up * rotateSpeed / 3 * Time.deltaTime);
            }
            anim.SetBool("isSprinting", true);
        }
        else anim.SetBool("isSprinting", false);
        if (isSliding2())
        {
            playerRb.transform.Translate(Vector3.forward * slidingSpeed * Time.deltaTime);
            anim.SetBool("isSprinting", true);
        }
        else anim.SetBool("isSprinting", false);





        if (health <= 0)
        {
            Die();
        }
        if(transform.position.y < -5)
        {
            Die();
        }

        if (Input.GetKey(KeyCode.F) && canShoot())
        {
            Shoot();
            
        }
        if (Input.GetKey(KeyCode.F))
        {
            anim.SetBool("isShooting", true);
        }
        else anim.SetBool("isShooting", false);
        if (rotateDoor)
        {
            
            door.transform.rotation = Quaternion.Lerp(Quaternion.Euler(door.transform.eulerAngles), Quaternion.Euler(test), Time.deltaTime);
        }
    }
    void Shoot()
    {
        Instantiate(bullet, transform.position + new Vector3(0, 1, 0), transform.rotation);
        timeStamp = Time.time;
    }
    void RoundShoot()
    {
        for(int i = 0; i < 16; i++)
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), bullet.transform.rotation);
            bullet.transform.Rotate(new Vector3(0, 22.5f, 0));
        }
        for(int i = 0; i < 16; i++)
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), bullet.transform.rotation);
            bullet.transform.Rotate(new Vector3(0, 0, 22.5f));
        }
        for (int i = 0; i < 16; i++)
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), bullet.transform.rotation);
            bullet.transform.Rotate(new Vector3(22.5f, 0, 0));
        }
        for (int i = 0; i < 16; i++)
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), bullet.transform.rotation);
            bullet.transform.Rotate(new Vector3(22.5f, 22.5f, 0));
        }
        for (int i = 0; i < 16; i++)
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), bullet.transform.rotation);
            bullet.transform.Rotate(new Vector3(22.5f, 0, 22.5f));
        }
        for (int i = 0; i < 16; i++)
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), bullet.transform.rotation);
            bullet.transform.Rotate(new Vector3(22.5f, 22.5f, 22.5f));
        }
        timeStamp = Time.time;
    }
    void Die()
    {
        dieSound.Play();
        Destroy(gameObject);
        spectator.enabled = true;
        restartText.enabled = true;
    }
    bool canShoot()
    {
        if (Time.time - timeStamp >= fireRate)
        {
            return true;
        }
        else return false;
    }
    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.transform.position,.3f,ground);
    }
    bool isSliding()
    {
        return Physics.CheckSphere(groundCheck.transform.position, .5f, ice);
    }
    bool isSliding2()
    {
        return Physics.CheckSphere(groundCheck.transform.position, .2f, ice2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            key++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("End"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        if (other.gameObject.CompareTag("Deadly"))
        {
            health -= 100;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Door") && key > 0)
        {
            key--;
            rotateDoor = true;
            //Destroy(collision.gameObject);
        }
    }
}
