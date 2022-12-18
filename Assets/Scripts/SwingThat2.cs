using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingThat2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float swingSpeed = 10f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Vector3.up * swingSpeed * Time.deltaTime);
    }
}
