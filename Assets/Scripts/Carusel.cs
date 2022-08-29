using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carusel : MonoBehaviour
{
    public float rotationSpeed = 20f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
    }
}
