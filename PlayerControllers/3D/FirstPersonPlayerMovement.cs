using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class FirstPersonPlayerMovement : MonoBehaviour {
	[Header("Movement")]
	public float speed = 10f;
	public float gravity = 9.8f;
	public float airControl = 0.5f;
	public float maxVelocityChange = 10f;
	public bool canJump = true;
	public float jumpHeight = 2f;
	[Header("Grounding")]
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float groundCheckRadius = 0.1f;
	[Header("Physics Materials")]
	public PhysicMaterial friction;
	public PhysicMaterial noFriction;

	private Rigidbody rb;
	private new CapsuleCollider collider;

	void Awake() {
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.useGravity = false;

		collider = GetComponent<CapsuleCollider>();
	}

	void FixedUpdate() {
		if(!GameManager.ins.playing) return;

		bool grounded = IsGrounded();

		if(grounded && collider.material != friction) collider.material = friction;
		else if(!grounded && collider.material != noFriction) collider.material = noFriction;

		Vector3 targetVelocity = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= speed;

		if(!grounded) targetVelocity *= airControl;

		Vector3 currentVelocity = rb.velocity;
		Vector3 deltaVelocity = (targetVelocity - currentVelocity);
		deltaVelocity.y = 0;
		
		if(deltaVelocity.magnitude > maxVelocityChange || deltaVelocity.magnitude < -maxVelocityChange) {
			deltaVelocity = deltaVelocity.normalized * maxVelocityChange;
		}

		rb.AddForce(deltaVelocity,ForceMode.VelocityChange);

		if(grounded && canJump && Input.GetButton("Jump")) {
			rb.velocity = new Vector3(currentVelocity.x,GetJumpVelocity(),currentVelocity.z);
		}

		rb.AddForce(new Vector3(0,-gravity * rb.mass, 0));
	}

	bool IsGrounded() {
		return Physics.OverlapSphere(groundCheck.position,groundCheckRadius,groundLayer.value).Length > 0;
	}

	float GetJumpVelocity() {
		return Mathf.Sqrt(2 * gravity * jumpHeight);
	}
}
