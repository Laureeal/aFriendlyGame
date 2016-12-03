using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float speed = 1;

    public float leftBorder = -51.2f;
    public float rightBorder = 51.2f;
    float width;

    void Start ()
    {
        width = rightBorder - leftBorder;
    }

    void Update ()
    {
        transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, transform.position.y, transform.position.z);

        if (transform.position.x < leftBorder)
        {
            transform.position = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);

        }
        if (transform.position.x > rightBorder)
        {
            transform.position = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);

        }
    }
}
