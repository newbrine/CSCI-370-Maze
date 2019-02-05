using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour { 

    public float speed;
    public bool destroyWall;
    public bool rotate;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        speed = 8;
        rotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rotate = true;
        } 
        if (rotate)
        {
            camera.transform.Rotate(-Input.GetAxis("Vertical") * Time.deltaTime * speed, 0f, 0f);
            if (Input.GetMouseButtonDown(1))
            {
                rotate = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Translate(0f, 3, 0f);
            }
            transform.Translate(-Input.GetAxis("Vertical") * Time.deltaTime * speed, 0f, Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "InnerWall" & destroyWall == true)
        {
            Destroy(collision.gameObject);
            destroyWall = false;
        } else if(collision.collider.tag == "Finish")
        {
            speed = 0;
        } else if(collision.collider.tag == "Teleport")
        {
            speed = 0;
        }
    }
}
