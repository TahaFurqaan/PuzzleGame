using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    //ball direction
    private string direction = "";
    private string directionLastFrame = "";
    //floor tracker
    [HideInInspector]
    public int onFloorTracker = 0;

    private bool fullSpeed = false;
    //speed variables
    private int floorSpeed = 130;
    private int airSpeed = 20;
    private float airSpeed_Diagonal = 5.858f;
    private float air_drag = 0.1f;
    private float floorDrag = 2.29f;
    private float delta = 50f;

    //camera variables
    private Vector3 cameraRelative_Right;
    private Vector3 cameraRelative_Up;
    private Vector3 camerRelative_Down;
    private Vector3 cameraRelative_Up_Right;
    private Vector3 cameraRelative_Up_Left;

    //velocity and magnitude variables
    private Vector3 x_Vel;
    private Vector3 z_Vel;
    private float x_speed;
    private float z_speed;

    //movement axis
    private string Axis_Y = "Vertical";
    private string Axis_X = "Horizontal";

    private Rigidbody myBody;
    private Camera mainCamera;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        FullSpeedController();
        DragAdujustmentAndAirSpeed();
        UpdateCameraRelativePosition();
        Getdirection();
        BallFellDown();
    }

    void FixedUpdate()
    {
        MoveTheBall();
    }

    void LateUpdate()
    {
        directionLastFrame = direction;
    }

    void Getdirection()
    {
        direction = "";

        //y-axis
        if (Input.GetAxis(Axis_Y) > 0)
        {
            direction += "up";
        }
        else if (Input.GetAxis(Axis_Y) < 0)
        {
            direction += "down";
        }
        
        //in x-Axis
        if(Input.GetAxis(Axis_X) > 0)
        {
            direction += "right";
        }
        else if(Input.GetAxis(Axis_X) < 0)
        {
            direction += "left";
        }
    }

    void MoveTheBall()
    {
        switch (direction)
        {
            case "upright":
                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        myBody.AddForce(floorSpeed * cameraRelative_Up_Right * Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        myBody.AddForce((floorSpeed - 75f) * cameraRelative_Up_Right * Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_Vel.normalized == cameraRelative_Up)
                    {
                        if (z_speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            myBody.AddForce(10.6f * cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                    }
                    else
                    {
                        myBody.AddForce(10.6f * cameraRelative_Up * Time.fixedDeltaTime * delta);
                    }

                    if (x_Vel.normalized == cameraRelative_Right)
                    {
                        if (x_speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            myBody.AddForce(10.6f * cameraRelative_Right * Time.fixedDeltaTime * delta);
                        }
                    }
                    else
                    {
                        myBody.AddForce(10.6f * cameraRelative_Right * Time.fixedDeltaTime * delta);
                    }
                }
                break;

            case "upleft":
                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        myBody.AddForce(floorSpeed * cameraRelative_Up_Left * Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        myBody.AddForce((floorSpeed - 75f) * cameraRelative_Up_Left * Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_Vel.normalized == cameraRelative_Up)
                    {
                        if (z_speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            myBody.AddForce(10.6f * cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                        else
                        {
                            myBody.AddForce(10.6f * cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                    }

                    if (x_Vel.normalized == -cameraRelative_Right)
                    {
                        if (x_speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            myBody.AddForce(10.6f * -cameraRelative_Right * Time.fixedDeltaTime * delta);
                        }
                    }
                    else
                    {
                        myBody.AddForce(10.6f * -cameraRelative_Right * Time.fixedDeltaTime * delta);
                    }
                }
                break;

            case "downright":
                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        myBody.AddForce(floorSpeed * -cameraRelative_Up_Left * Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        myBody.AddForce((floorSpeed - 75f) * -cameraRelative_Up_Left * Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_Vel.normalized == -cameraRelative_Up)
                    {
                        if (z_speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            myBody.AddForce(10.6f * -cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                        else
                        {
                            myBody.AddForce(10.6f * -cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                    }

                    if (x_Vel.normalized == cameraRelative_Right)
                    {
                        if (x_speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            myBody.AddForce(10.6f * cameraRelative_Right * Time.fixedDeltaTime * delta);
                        }
                    }
                    else
                    {
                        myBody.AddForce(10.6f * cameraRelative_Right * Time.fixedDeltaTime * delta);
                    }
                }
                break;

            case "downleft":
                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        myBody.AddForce(floorSpeed * -cameraRelative_Up_Right * Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        myBody.AddForce((floorSpeed - 75f) * -cameraRelative_Up_Right * Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_Vel.normalized == -cameraRelative_Up)
                    {
                        if (z_speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            myBody.AddForce(10.6f * -cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                        else
                        {
                            myBody.AddForce(10.6f * -cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                    }

                    if (x_Vel.normalized == -cameraRelative_Right)
                    {
                        if (x_speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            myBody.AddForce(10.6f * -cameraRelative_Right * Time.fixedDeltaTime * delta);
                        }
                    }
                    else
                    {
                        myBody.AddForce(10.6f * -cameraRelative_Right * Time.fixedDeltaTime * delta);
                    }          
                }
                break;

            case "up":
                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        myBody.AddForce(floorSpeed * cameraRelative_Up * Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        myBody.AddForce((floorSpeed - 75f) * cameraRelative_Up * Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_speed < airSpeed)
                    {
                        myBody.AddForce((airSpeed * 0.75f) * cameraRelative_Up * Time.fixedDeltaTime * delta);
                    }

                    if (x_speed > 0.1f)
                    {
                        if (x_Vel.normalized == cameraRelative_Right)
                        {
                            myBody.AddForce((airSpeed * 0.75f) * -cameraRelative_Right * Time.fixedDeltaTime * delta);
                        }
                        else if (x_Vel.normalized == -cameraRelative_Right)
                        {
                            myBody.AddForce((airSpeed * 0.75f) * cameraRelative_Right * Time.fixedDeltaTime * delta);
                        }
                    }
                }
                break;

            case "down":
                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        myBody.AddForce(floorSpeed * -cameraRelative_Up * Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        myBody.AddForce((floorSpeed - 75f) * -cameraRelative_Up * Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_speed < airSpeed)
                    {
                        myBody.AddForce((airSpeed * 0.75f) * -cameraRelative_Up * Time.fixedDeltaTime * delta);
                    }

                    if (x_speed > 0.1f)
                    {
                        if (x_Vel.normalized == cameraRelative_Right)
                        {
                            myBody.AddForce((airSpeed * 0.75f) * -cameraRelative_Right * Time.fixedDeltaTime * delta);
                        }
                        else if (x_Vel.normalized == -cameraRelative_Right)
                        {
                            myBody.AddForce((airSpeed * 0.75f) * cameraRelative_Right * Time.fixedDeltaTime * delta);
                        }
                    }
                }
                break;

            case "right":
                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        myBody.AddForce(floorSpeed * cameraRelative_Right * Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        myBody.AddForce((floorSpeed - 75f) * cameraRelative_Right * Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (x_speed < airSpeed)
                    {
                        myBody.AddForce((airSpeed * 0.75f) * cameraRelative_Right * Time.fixedDeltaTime * delta);
                    }

                    if (z_speed > 0.1f)
                    {
                        if (z_Vel.normalized == cameraRelative_Up)
                        {
                            myBody.AddForce((airSpeed * 0.75f) * -cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                        else if (z_Vel.normalized == -cameraRelative_Up)
                        {
                            myBody.AddForce((airSpeed * 0.75f) * cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                    }
                }
                break;

            case "left":
                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        myBody.AddForce(floorSpeed * -cameraRelative_Right * Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        myBody.AddForce((floorSpeed - 75f) * -cameraRelative_Right * Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (x_speed < airSpeed)
                    {
                        myBody.AddForce((airSpeed * 0.75f) * -cameraRelative_Right * Time.fixedDeltaTime * delta);
                    }

                    if (z_speed > 0.1f)
                    {
                        if (z_Vel.normalized == cameraRelative_Up)
                        {
                            myBody.AddForce((airSpeed * 0.75f) * -cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                        else if (z_Vel.normalized == -cameraRelative_Up)
                        {
                            myBody.AddForce((airSpeed * 0.75f) * cameraRelative_Up * Time.fixedDeltaTime * delta);
                        }
                    }
                }
                break;
        }
    }

    void UpdateCameraRelativePosition()
    {
        //Right
        cameraRelative_Right = mainCamera.transform.TransformDirection(Vector3.right);

        //Up means moving forward in z- axis
        cameraRelative_Up = mainCamera.transform.TransformDirection(Vector3.forward);
        cameraRelative_Up.y = 0f;
        cameraRelative_Up = cameraRelative_Up.normalized;

        //Up_Right
        cameraRelative_Up_Right = (cameraRelative_Up + cameraRelative_Right);
        cameraRelative_Up_Right = cameraRelative_Up_Right.normalized;

        //Up_Left
        cameraRelative_Up_Left = (cameraRelative_Up - cameraRelative_Right);
        cameraRelative_Up_Left = cameraRelative_Up_Left.normalized;
    }

    void FullSpeedController()
    {
        if(direction != directionLastFrame)
        {
            if(direction == "")
            {
                StopCoroutine("FullSpeedTimer");
                fullSpeed = false;
            }
            else if(directionLastFrame == "")
            {
                StartCoroutine("FullSpeedTimer");
            }
        }
    }

    IEnumerator FullSpeedTimer()
    {
        yield return new WaitForSeconds(0.07f);
        fullSpeed = true;
    }

    void DragAdujustmentAndAirSpeed()
    {
        //on floor
        if(onFloorTracker > 0)
        {
            myBody.drag = floorDrag;
        }
        //in air
        else if(onFloorTracker == 0)
        {
            x_Vel = Vector3.Project(myBody.velocity, cameraRelative_Right);
            z_Vel = Vector3.Project(myBody.velocity, cameraRelative_Up);

            x_speed = x_Vel.magnitude;
            z_speed = z_Vel.magnitude;

            myBody.drag = air_drag;
        } 
    }

    void BallFellDown()
    {
        if(transform.position.y < -25f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Floor")
        {
            onFloorTracker++;
        }
    }

    void OnCollisionExit(Collision target)
    {
        if(target.gameObject.tag == "Floor")
        {
            onFloorTracker--;
        }
    }
}
