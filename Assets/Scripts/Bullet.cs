using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public AudioSource bulletSource;
    public AudioClip bulletClip;
    private void Awake()
    {
        bulletSource.PlayOneShot(bulletClip);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
