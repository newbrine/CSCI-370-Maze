using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour { 

    private float speed;
    private bool destroyWall;
    private bool rotate;
    public Camera camera;
    public Camera rotationCamera;
    public Rigidbody rbCap;
    private bool aClick;
    private bool sClick;
    private bool dClick;
    private float facing;
    public TextMesh bottom1;
    public TextMesh bottom2;
    public TextMesh bottom3;
    public TextMesh bottom4;
    public TextMesh top1;
    public TextMesh top2;
    public TextMesh top3;
    public TextMesh top4;
    public Plane minimapCoverOne;
    public Plane minimapCoverTwo;
    


    // Start is called before the first frame update
    void Start()
    {
        destroyWall = true;
        speed = 8;
        facing = 0;
        rotate = false;
        aClick = false;
        sClick = false;
        dClick = false;
        bottom1.gameObject.SetActive(false);
        bottom2.gameObject.SetActive(false);
        bottom3.gameObject.SetActive(false);
        bottom4.gameObject.SetActive(false);
        top1.gameObject.SetActive(false);
        top2.gameObject.SetActive(false);
        top3.gameObject.SetActive(false);
        top4.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rotate = true;
        }
        else if (rotate)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                rotationCamera.transform.Rotate(0f, 180f, 0f);
                sClick = true;
                setDirection("S");
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                rotationCamera.transform.Rotate(0f, 90f, 0f);
                aClick = true;
                setDirection("A");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                rotationCamera.transform.Rotate(0f, 270f, 0f);
                dClick = true;
                setDirection("D");
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (sClick)
                {
                    rotationCamera.transform.Rotate(0f, -180f, 0f);
                    sClick = false;
                } 
                else if (aClick)
                {
                    rotationCamera.transform.Rotate(0f, -90f, 0f);
                    aClick = false;
                }
                else if (dClick)
                {
                    rotationCamera.transform.Rotate(0f, -270f, 0f);
                    dClick = false;
                }
                setDirection("W");
            }
            if (Input.GetMouseButtonDown(1))
            {
                rotate = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            rbCap.AddForce(0, 500, 0);
        }
        else
        {
            if (facing == 180)
            {
                transform.Translate(Input.GetAxis("Vertical") * Time.deltaTime * speed, 0f, -Input.GetAxis("Horizontal") * Time.deltaTime * speed);
            }
            else if (facing == 90)
            {
                transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
            }
            else if (facing == 270)
            {
                transform.Translate(-Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, -Input.GetAxis("Vertical") * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(-Input.GetAxis("Vertical") * Time.deltaTime * speed, 0f, Input.GetAxis("Horizontal") * Time.deltaTime * speed);
            }
            
        }
         
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "InnerWall")
        {
            if (destroyWall)
            {
                Destroy(collision.gameObject);
                destroyWall = false;
            }
            
        }
        else if (collision.collider.tag == "Finish")
        {
            if (collision.collider.name == "FloorOneWin")
            {
                Invoke("ShowBottomFloorWinText", 1f);
            }
            else if (collision.collider.name == "FloorTwoWin")
            {
                Invoke("ShowTopFloorWinText", 1f);
            }
            speed = 0;
        }
        else if (collision.collider.tag == "Teleport")
        {
            if (collision.collider.name == "Teleport1")
            {
                transform.position = new Vector3(7.3f, 1.5f, 71f);
            }
            else if (collision.collider.name == "Teleport2")
            {
                transform.position = new Vector3(9.76f, 1.5f, 48.47f);
            }
            else if (collision.collider.name == "Teleport3")
            {
                transform.position = new Vector3(-16.52f, 1.44f, 57.26f);
            }
            else if (collision.collider.name == "HiddenTeleport")
            {
                transform.position = new Vector3(9.6f, 1.5f, 56.31f);
            }
            camera.transform.position = new Vector3(-3.2f, 28f, 53.6f);
        }
        else if (collision.collider.tag == "FloorTeleport")
        {
            if (collision.collider.name == "Teleport1Top")
            {
                transform.position = new Vector3(4f, 1.5f, 10f);
            }
            else if (collision.collider.name == "Teleport2Top")
            {
                transform.position = new Vector3(9.82f, 1.5f, -15.9f);
            }
            else if (collision.collider.name == "Teleport3Top")
            {
                transform.position = new Vector3(-12.16f, 1.5f, -5.6f);
            }
            else if (collision.collider.name == "HiddenTeleportTop")
            {
                transform.position = new Vector3(6.1f, 1.5f, -6.35f);
            }
            else if (collision.collider.name == "HiddenWinTeleport")
            {
                transform.position = new Vector3(-14f, 1.5f, 6.78f);
            }
            camera.transform.position = new Vector3(-2.97f, 27.94f, -8.72f);
        }
        else if (collision.collider.tag == "BreakBlock")
        {
            destroyWall = true;
            Destroy(collision.gameObject);
        }
    }

    void ShowBottomFloorWinText()
    {
        bottom1.gameObject.SetActive(true);
        bottom2.gameObject.SetActive(true);
        bottom3.gameObject.SetActive(true);
        bottom4.gameObject.SetActive(true);
    }

    void ShowTopFloorWinText()
    {
        top1.gameObject.SetActive(true);
        top2.gameObject.SetActive(true);
        top3.gameObject.SetActive(true);
        top4.gameObject.SetActive(true);
    }

    void setDirection(string text)
    {
        if(text == "A")
        {
            facing = facing + 90;
        }
        else if(text == "D")
        {
            facing = facing + 270;
        }
        else if(text == "S")
        {
            facing = facing + 180;
        }
        else if(text == "W")
        {
            facing = 0;
        }
        facing = facing % 360;
    }
}
