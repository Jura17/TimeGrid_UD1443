using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acceleration : MonoBehaviour {

    public Rigidbody rb;
    public float topSpeed = 20f;
    public float Strength = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey("w"))
        {

            // transform.Translate(10 * Vector3.forward * Time.deltaTime);
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
       
        if(Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * Time.deltaTime);

        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * Time.deltaTime);

        }
    }
}
