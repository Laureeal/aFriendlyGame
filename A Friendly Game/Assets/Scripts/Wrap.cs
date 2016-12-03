using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MonoBehaviour {
    public float leftBorder = -51.2f;
    public float rightBorder = 51.2f;
    public bool copy = true;
    float width;

    void Start () {
        width = rightBorder - leftBorder;
        if (copy){
            GameObject left = Instantiate<GameObject>(gameObject, new Vector3(transform.position.x - width, transform.position.y, transform.position.z),transform.rotation);
            GameObject right = Instantiate<GameObject>(gameObject, new Vector3(transform.position.x + width, transform.position.y, transform.position.z), transform.rotation);
            left.transform.SetParent(transform);
            right.transform.SetParent(transform);
            Destroy(left.GetComponent<Wrap>());
            Destroy(right.GetComponent<Wrap>());

        }
    }
	
	void Update ()
    {
        
        if (transform.position.x < leftBorder)
        {
            transform.position = new Vector3(transform.position.x +width, transform.position.y, transform.position.z) ;

        }
        if (transform.position.x > rightBorder)
        {
            transform.position = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);

        }

        
    }
}
