using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform ballTransform;

    public float distance = 50f;
    public float xSpeed = 250f;
    public float ySpeed = 120f;
    public float yMinLimit = 0f;
    public float yMaxLimit = 80f;

    private Quaternion rotation;
    private Vector3 position;

    private float xAngel = 0f, yAngel = 0f;
    private float angelMultiplier = 0.02f;

    private bool snapCameraPosition;

    void Awake()
    {
        ballTransform = GameObject.Find("Ball").transform;
    }

    void Start()
    {
        Vector3 rot = transform.eulerAngles;
        yAngel = rot.x;
        xAngel = rot.y;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            snapCameraPosition = true;
        }
    }

    void LateUpdate()
    {
        if(ballTransform)
        {
            xAngel += Input.GetAxis("Mouse X") * xSpeed * angelMultiplier;
            yAngel += Input.GetAxis("Mouse Y") * ySpeed * angelMultiplier;

            yAngel = ClampAngel(yAngel, yMinLimit, yMaxLimit);
            //we are snaping camera position by using y degrees
            if(snapCameraPosition)
            {
                if ((transform.eulerAngles.y <= 225f) && (transform.eulerAngles.y > 135f))
                {
                    xAngel = 180f;
                }
                else if ((transform.eulerAngles.y <= 135) && (transform.eulerAngles.y > 45f))
                {
                    xAngel = 90f;
                }
                else if ((transform.eulerAngles.y <= 315f) && (transform.eulerAngles.y > 225f))
                {
                    xAngel = 270f;
                }
                else
                {
                    xAngel = 0f;
                }
                snapCameraPosition = false;
            }
            rotation = Quaternion.Euler(yAngel, xAngel, 0);
            position = rotation * new Vector3(0, 0, -distance) + ballTransform.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    float ClampAngel(float angel,float min, float max)
    {
        if (angel < -360f)
            angel += 360f;

        if (angel > 360f)
            angel -= 360f;

        return Mathf.Clamp(angel, min, max);
    }
}
