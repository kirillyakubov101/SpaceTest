using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
	[Header("Speed")]
	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 1000f;
	[Header("Sounds")]
	[SerializeField] AudioClip EngineSFX;
	[SerializeField] AudioClip DeathSFX;
	[SerializeField] AudioClip WinSFX;
	[Header("Particles")]
	[SerializeField] ParticleSystem DeathVFX;
	[SerializeField] ParticleSystem WinVFX;
	[SerializeField] ParticleSystem EngineVFX;
	[Header("Other")]
	[SerializeField] float LevelLoadDelay = 3f;

	//config
	Rigidbody myRigidbody;
	AudioSource audioSource;

	//STATES
	enum State { ALIVE, DEAD, TRANSENCDING }
	State state = State.ALIVE;
	bool collisionsDisabled = false;

	// Start is called before the first frame update
	void Start()
	{
		myRigidbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Debug.isDebugBuild)
		{
			DebugKeys();
		}
		

		if (state == State.ALIVE)
		{
			Thrust();
			Rotate();
		}
	}

	private void Rotate()
	{
		myRigidbody.angularVelocity = Vector3.zero;
		float RotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * RotationThisFrame);
		}

		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward * RotationThisFrame);
		}
	}

	private void Thrust()
	{
		float RotationThisFrame = mainThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.Space))
		{
			myRigidbody.AddRelativeForce(Vector3.up * RotationThisFrame);
			EngineVFX.Play();
			if (audioSource.isPlaying == false)
			{
				audioSource.PlayOneShot(EngineSFX);
			}

		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (state != State.ALIVE || collisionsDisabled) { return; } // so it won't get called more than 1

		switch (collision.gameObject.tag)
		{
			case "Friendly":
				//do noting
				break;

			case "Finish":
				HandleWin();
				break;

			default:
				HandleDeath();
				break;

		}
	}

	private void HandleWin()
	{
		state = State.TRANSENCDING;
		audioSource.Stop();
		EngineVFX.Stop();
		WinVFX.Play();
		audioSource.PlayOneShot(WinSFX);
		Invoke("LoadNextScene", LevelLoadDelay);
	}

	private void HandleDeath()
	{
		state = State.DEAD;
		audioSource.Stop();
		EngineVFX.Stop();
		DeathVFX.Play();
		audioSource.PlayOneShot(DeathSFX);
		Invoke("LoadFirstScene", LevelLoadDelay);
	}

	private void LoadNextScene()
	{
		int CurrentScene = SceneManager.GetActiveScene().buildIndex;
		int nextScene = CurrentScene + 1;
		if(nextScene == SceneManager.sceneCountInBuildSettings) { nextScene = 0; }
		SceneManager.LoadScene(nextScene);
	}

	private void LoadFirstScene()
	{
		SceneManager.LoadScene(0);
	}

	private void DebugKeys()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			LoadNextScene();
		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			collisionsDisabled = !collisionsDisabled;
		}
	}
}
