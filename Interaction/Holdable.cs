using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : Interactable {
	public string friendlyName;
	public PhysicMaterial holdingMaterial;

	private PhysicMaterial normalMaterial;
	private bool useGravity;
	private Transform player;
	
	void Start() {
		useGravity = GetComponent<Rigidbody>().useGravity;
		normalMaterial = GetComponent<Collider>().material;
	}

	void FixedUpdate() {
		if(player != null) {
			transform.LookAt(player);
		}
	}

	public override void Use(Player player) {
		if(player == null) return;
		player.RequestHold(this);
	}

	public void OnStartHold(Transform player) {
		this.player = player;

		Rigidbody rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.freezeRotation = true;

		GetComponent<Collider>().material = holdingMaterial;

	}

	public void OnEndHold() {
		this.player = null;

		Rigidbody rb = GetComponent<Rigidbody>();
		rb.useGravity = useGravity;
		rb.freezeRotation = false;
		GetComponent<Collider>().material = normalMaterial;
	}
}
