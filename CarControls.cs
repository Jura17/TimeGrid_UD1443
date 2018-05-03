using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{
	public WheelCollider FL_Col, FR_Col, BL_Col, BR_Col;
    public float SteerForce;
	public float topSpeed = 20f;
	public float Strength = 10f;
	public float SpeedBoostCount = 3;
	public float BoostStrengh = 10f;
	public float hangTimeSteerForce = 20;
    public float downForce = 0f;
	public CollisionCheck colliderObj;
    public float speedReducerStrength;

	private Rigidbody car_rb;
    private float DriftForce;
    private float velocityMagnitude = 0;
	private bool isColliding = true;
    private float initialLinearDrag;
	private Vector3 v3Force;
	private Vector3 v3SpeedBoost;


	void Start()
	{
		car_rb = GetComponent<Rigidbody>();
        initialLinearDrag = car_rb.drag;
	}

    void FixedUpdate()
	{
        AddDownForce();

        isColliding = colliderObj.getIsColliding();
		if (isColliding)
		{
            //car is in contact with racing track
            velocityMagnitude = car_rb.velocity.magnitude;
			float horizontal = Input.GetAxis("Horizontal") * SteerForce;
			float horizontal2 = Input.GetAxis("Horizontal") * DriftForce;
			FL_Col.steerAngle = horizontal;
			FR_Col.steerAngle = horizontal;
			BL_Col.steerAngle = -horizontal2;
			BR_Col.steerAngle = -horizontal2;


			//forward thrust
			v3Force = Strength * transform.forward * Time.deltaTime;
			v3SpeedBoost = Strength * 2 * transform.forward * Time.deltaTime;
			if (Input.GetKey("w"))
			{
				car_rb.AddForce(v3Force);
				car_rb.velocity = Vector3.ClampMagnitude(car_rb.velocity, topSpeed);
			}

			if (Input.GetKey("s"))
			{
				car_rb.AddForce(-v3Force);
				car_rb.velocity = Vector3.ClampMagnitude(car_rb.velocity, topSpeed);
			}
            
            if (Input.GetKeyUp(KeyCode.Space) && Strength == 8500000)
            {
				if (SpeedBoostCount > 0)
				{
					Strength = Strength * BoostStrengh;
					car_rb.AddForce(v3Force);

					SpeedBoostCount--;
				}
				StartCoroutine("CallAfterTime");
			}

			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				DriftForce = 40f;
			}
			if (Input.GetKeyUp(KeyCode.LeftShift))
			{
				DriftForce = 0f;
			}
		}
		else
		{
			//car is in air
			v3Force = Strength/1.5f * transform.forward * Time.deltaTime;
			v3SpeedBoost = Strength * 2 * transform.forward * Time.deltaTime;
			if (Input.GetKey("w"))
			{
				car_rb.AddForce(v3Force);
				car_rb.velocity = Vector3.ClampMagnitude(car_rb.velocity, topSpeed);
			}

            
            if (Input.GetKeyUp(KeyCode.Space) && Strength == 8500000)
            {
				if (SpeedBoostCount > 0)
				{
					Strength = Strength * BoostStrengh;
					car_rb.AddForce(v3Force);

					SpeedBoostCount--;
				}
				StartCoroutine("CallAfterTime");
			}
			if (Input.GetKey("d"))
				transform.Rotate(0, hangTimeSteerForce * Time.deltaTime, 0);

			if (Input.GetKey("a"))
				transform.Rotate(0, -hangTimeSteerForce * Time.deltaTime, 0);
		}
	}

    private void AddDownForce()
    {
        car_rb.AddRelativeForce(-transform.up * downForce * velocityMagnitude);
    }

    //------SPEED REDUCER-------
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("SpeedReducer"))
        {
            car_rb.drag = speedReducerStrength;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedReducer"))
        {
            car_rb.drag = initialLinearDrag;
        }   
    }

	IEnumerator CallAfterTime()
	{
		yield return new WaitForSeconds(3);
		Strength = Strength / BoostStrengh;
	}
}
