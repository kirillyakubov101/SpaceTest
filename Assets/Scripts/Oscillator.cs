using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //sick method. only one script is allowed

public class Oscillator : MonoBehaviour
{
	[SerializeField] Vector3 movementVector;
	[Range(0, 1)] [SerializeField] float movementFactor;

	Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
		startingPos = transform.position;

	}

    // Update is called once per frame
    void Update()
    {
		Vector3 Offset = movementVector * movementFactor;
		transform.position = startingPos + Offset;
    }
}
