using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingThat: MonoBehaviour
{
    public float swingSpeed = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * swingSpeed * Time.deltaTime);
    }
}
