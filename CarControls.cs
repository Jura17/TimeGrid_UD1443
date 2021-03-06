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
    Vector3 v3Force;
    Vector3 v3SpeedBoost;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * SteerForce;
        float horizontal2 = Input.GetAxis("Horizontal") * SteerForce2;
        FL_Col.steerAngle = horizontal;
        FR_Col.steerAngle = horizontal;
        BL_Col.steerAngle = -horizontal2;
        BR_Col.steerAngle = -horizontal2;



        //forward thrust
        v3Force = Strength * transform.forward * Time.deltaTime; //sets forward vector
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
            StartCoroutine("CallAfterTime"); // waits for 3 sec
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

    IEnumerator CallAfterTime() 
    {

        yield return new WaitForSeconds(3);
        Strength = Strength / BoostStrengh;
    }
}
