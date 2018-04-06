using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour {

    public WheelCollider F_Left, F_Right, B_Left, B_Right;
    public float SteerForce, MotorForce, BrakeForce;
    public Rigidbody rb;
    public float topSpeed = 20f;
    public float Strength = 10f;
    void Start () {
   
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal") * SteerForce ;
		float vertical = Input.GetAxis ("Vertical") * MotorForce ;

        //Debug.Log(vertical);
		//B_Left.motorTorque = vertical;
		//B_Right.motorTorque = vertical;

        F_Left.steerAngle = horizontal;
        F_Right.steerAngle = horizontal;


		if (Input.GetKey (KeyCode.Space)) 
		{
			B_Left.brakeTorque = BrakeForce;
			B_Right.brakeTorque = BrakeForce;
		}

		if(Input.GetKeyUp(KeyCode.Space))
		{
			B_Left.brakeTorque = 0;
			B_Right.brakeTorque = 0;
    	}

		if (Input.GetAxis ("Vertical") == 0) {
			B_Left.brakeTorque = BrakeForce;
			B_Right.brakeTorque = BrakeForce;
		} else {
			B_Left.brakeTorque = 0;
			B_Right.brakeTorque = 0;
		}

        //forward thrust
        if (Input.GetKey("w"))
        {


            Vector3 v3Force = Strength * transform.forward * Time.deltaTime;
            rb.AddForce(v3Force);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, topSpeed);

        }

        if (Input.GetKey("s"))
        {

            Vector3 v3Force = -Strength * transform.forward * Time.deltaTime;
            rb.AddForce(v3Force);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, topSpeed);
        }
        if (Input.GetKey("a"))
        {
            transform.Rotate(0, horizontal * Time.deltaTime, 0);
            Debug.Log(horizontal);
        }
        if (Input.GetKey("d"))
        {
            transform.Rotate(0, horizontal * Time.deltaTime, 0);
            Debug.Log(horizontal);
        }

    }
}
