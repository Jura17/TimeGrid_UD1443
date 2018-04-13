using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    public WheelCollider FL_Col, FR_Col, BL_Col, BR_Col;
    public float SteerForce, SteerForce2, MotorForce, BrakeForce;
    public Rigidbody rb;
    public float downForce = 111.81f;
    public float topSpeed = 20f;
    public float Strength = 10f;
    public float SpeedBoostCount = 3;
    public float BoostStrengh = 10f;
    public float hangTimeSteerForce = 20;
    Vector3 v3Force;
    Vector3 v3SpeedBoost;
    private bool isColliding = false;
    public CollisionCheck colliderObj;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    void FixedUpdate()
    {
        isColliding = colliderObj.getIsColliding();
        Debug.Log(isColliding);
        if (isColliding) {
            //car is in contact with racing track

            float horizontal = Input.GetAxis("Horizontal") * SteerForce;
            float horizontal2 = Input.GetAxis("Horizontal") * SteerForce2;
            FL_Col.steerAngle = horizontal;
            FR_Col.steerAngle = horizontal;
            BL_Col.steerAngle = -horizontal2;
            BR_Col.steerAngle = -horizontal2;


            //forward thrust
            v3Force = Strength * transform.forward * Time.deltaTime;
            v3SpeedBoost = Strength * 2 * transform.forward * Time.deltaTime;
            if (Input.GetKey("w"))
            {
                rb.AddForce(v3Force);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, topSpeed);
            }

            if (Input.GetKey("s"))
            {
                rb.AddForce(-v3Force);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, topSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Space) && Strength == 5000000)
            {
                if (SpeedBoostCount > 0)
                {
                    Strength = Strength * BoostStrengh;
                    rb.AddForce(v3Force);

                    SpeedBoostCount--;
                }
                StartCoroutine("CallAfterTime");
                print(Strength);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SteerForce2 = 60f;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                SteerForce2 = 0f;
            }
        }
        else {
        //car is in air
            v3Force = Strength * transform.forward * Time.deltaTime;
            v3SpeedBoost = Strength * 2 * transform.forward * Time.deltaTime;
            if (Input.GetKey("w"))
            {
                rb.AddForce(v3Force);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, topSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Space) && Strength == 5000000)
            {
                if (SpeedBoostCount > 0)
                {
                    Strength = Strength * BoostStrengh;
                    rb.AddForce(v3Force);

                    SpeedBoostCount--;
                }
                StartCoroutine("CallAfterTime");
                print(Strength);
            }
            if(Input.GetKey("d"))
            transform.Rotate(0, hangTimeSteerForce * Time.deltaTime, 0);

            if (Input.GetKey("a"))
            transform.Rotate(0, -hangTimeSteerForce * Time.deltaTime, 0);

        }
    }


    IEnumerator CallAfterTime()
    {
        yield return new WaitForSeconds(3);
        Strength = Strength / BoostStrengh;
    }
}
