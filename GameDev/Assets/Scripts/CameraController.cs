using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float rotationSpeed = 1f;
	public float moveSpeed = 1f;
	public float tiltMax = 80f;
	public float tilfMin = 60f;

	Transform player;
	Transform pivot;

	Vector3 offset;
	Quaternion targetHorizontalRotation;
	Quaternion targetVerticalRotation;
	float horizontalRotation;
	float verticalRotation;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		pivot = GetComponentInChildren<Camera> ().transform.parent;

		offset = transform.position - player.position;
	}

	void Update () {
		HandleRotation ();
		HandleMovement ();

		//cursor lock
		if (Input.GetMouseButton (0))
			Cursor.lockState = CursorLockMode.Locked;
		if (Input.GetKeyDown (KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
	}

	void HandleRotation(){

		float x = Input.GetAxis ("Mouse X");
		float y = Input.GetAxis ("Mouse Y");

		//određivanje horizontalne rotacije
		horizontalRotation += rotationSpeed * x;
		targetHorizontalRotation = Quaternion.Euler (0f, horizontalRotation, 0f);

		//određivanje vertikalne rotacije
		verticalRotation -= rotationSpeed * y;
		verticalRotation = Mathf.Clamp (verticalRotation, -tilfMin, tiltMax);
		targetVerticalRotation = Quaternion.Euler (verticalRotation, 0f, 0f);

		//rotacija
		transform.localRotation = targetHorizontalRotation;
		pivot.localRotation = targetVerticalRotation;
	}

	void HandleMovement(){
		transform.position = player.position + offset;
	}
}
