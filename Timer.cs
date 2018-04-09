using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    //Apply this script to the car and create tags on the power ups of the same string name

    public float countdown;
    public int speedBoost;
    public Text countdownText;
    public Text speedBoostText;
    public float timeExtenderValue;
    public float timeReducerValue;

    void Start ()
    {
        countdownText.text = "Countdown: " + countdown.ToString();
        speedBoostText.text = "Speedboosts left: " + speedBoost.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TimeExtender"))
        {
            countdown += timeExtenderValue;
        }

        else if (other.gameObject.CompareTag("TimeReducer"))
        {
            countdown -= timeReducerValue;
        }

        else if (other.gameObject.CompareTag("SpeedBoost"))
        {

            //if (speedBoost < 0)
            //{
            //    speedBoostText.text = "Speedboosts left: 0";
            //}

            if (speedBoost >= 5)
            {
                speedBoostText.text = "Speedboosts left: 5";
            }

            else
            {
                speedBoost++;
                speedBoostText.text = "Speedboosts left: " + speedBoost.ToString();
            }
        }
    }



    // Update is called once per frame
    void Update () {

        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            countdownText.text = "Countdown: 00.00 ";
        }

        else
        {
            countdownText.text = "Countdown: " + countdown.ToString("F2");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (speedBoost <= 0)
            {
                speedBoostText.text = "Speedboosts left: 0";
            }

            else
            {
                speedBoost--;
                speedBoostText.text = "Speedboosts left: " + speedBoost.ToString();
            }
        }
    }
}
