using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	//config
	Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();

	}

    // Update is called once per frame
    void Update()
    {
		ProcessInput();
    }

	private void ProcessInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			myRigidbody.AddRelativeForce(Vector3.up);
		}

		if (Input.GetKey(KeyCode.A))
		{
			print("rotate Left");
		}

		else if (Input.GetKey(KeyCode.D))
		{
			print("rotate Right");
		}
	}
}
