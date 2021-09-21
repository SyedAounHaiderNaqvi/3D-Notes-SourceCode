using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    public Transform cam;
    public float movementTime;
    public float movementSpeed;
    public Vector3 pos;
    float fastSpeed = 2f;
    float slowSpeed = 0.3f;
    float fastTime = 10f;
    float slowTime = 7f;

    void Awake()
    {
        pos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementinput();
    }

    void HandleMovementinput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
            movementTime = fastTime;
            movementSpeed = fastSpeed;
		} else
		{
            movementTime = slowTime;
            movementSpeed = slowSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            pos += (transform.up * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            pos += (transform.up * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            pos += (transform.right * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            pos += (transform.right * movementSpeed);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            //zoom out
            pos += (transform.forward * -movementSpeed * 3);

        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            pos += (transform.forward * movementSpeed * 3);
        }

        if (pos.z > -10f)
        {
            pos = new Vector3(pos.x, pos.y, -10f);
        }
        if (pos.z < -40f)
        {
            pos = new Vector3(pos.x, pos.y, -40f);
        }

        if (pos.x > 350)
            pos.x = 349;

        if (pos.y > 320)
            pos.y = 319;

        if (pos.x < -720)
            pos.x = -719;

        if (pos.y < -430)
            pos.y = -429;

        if (Input.GetKeyDown(KeyCode.F))
        {
            pos.x = 0;
            pos.y = 0;
        }

        cam.position = Vector3.Lerp(cam.position, pos, Time.deltaTime * movementTime);
    }
}
