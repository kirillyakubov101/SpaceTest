using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 1000f;

	//config
	Rigidbody myRigidbody;
	AudioSource audioSource;
	

    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();

	}

    // Update is called once per frame
    void Update()
    {
		Thrust();
		Rotate();
	}

	private void Rotate()
	{
		myRigidbody.freezeRotation = true;
		float RotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * RotationThisFrame);
		}

		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward * RotationThisFrame);
		}

		myRigidbody.freezeRotation = false;
	}

	private void Thrust()
	{
		float RotationThisFrame = mainThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.Space))
		{
			myRigidbody.AddRelativeForce(Vector3.up * RotationThisFrame);
			if (audioSource.isPlaying == false)
			{
				audioSource.Play();
			}
			

		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		/*var OtherObject = collision.gameObject;

		if(OtherObject.tag != "Friendly")
		{
			Debug.Log("dead");
		}												my code

		else
		{
			Debug.Log("all good");
		}*/

		switch (collision.gameObject.tag)
		{
			case "Friendly":
				//do noting
				break;
			default:
				Debug.Log("dead");
				break;

		}
	}
}
