using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour {
	public new Transform camera;
	public float speed = 10f;
	public float clampX = 90;
	public float clampY = 75;

	private Vector2 rotation = Vector2.zero;
	private Quaternion originalRotation;
	private Quaternion originalCamRotation;
	
	void Start() {
		originalRotation = transform.localRotation;
		originalCamRotation = camera.localRotation;
	}

	void FixedUpdate() {
		if(!GameManager.ins.playing) return;
		
		Cursor.lockState = CursorLockMode.Locked;
		rotation.x += Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime;
		rotation.y += -Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime;
		
		rotation.x = WrapAndClamp(rotation.x,-clampX,clampX);
		rotation.y = WrapAndClamp(rotation.y,-clampY,clampY);

		Quaternion xQuat = Quaternion.AngleAxis(rotation.x,Vector3.up);
		Quaternion yQuat = Quaternion.AngleAxis(rotation.y,Vector3.right);

		transform.localRotation = originalRotation * xQuat;
		camera.localRotation = originalCamRotation * yQuat;
	}

	float WrapAndClamp(float angle, float min, float max) {
		if(angle < -360f) angle += 360f;
		else if (angle > 360f) angle -= 360f;

		return Mathf.Clamp(angle,min,max);
	}
}
