using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float speed = 1;
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
