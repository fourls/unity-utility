using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHoldablePlayer : MonoBehaviour {
	[Header("Interaction")]
	public LayerMask interactionLayer;
	public float interactionDistance;
	[Header("Holdables")]
	public Transform holdableTarget;
	public float maxHoldableDistance = 0.5f;
	public float holdableSnapSpeed = 5f;
	[HideInInspector]
	public bool interactCrosshairActive;
	private StateMachine<Player> sm;

	void Start() {
		sm = new StateMachine<Player>(this);
		sm.Next(new NotHoldingState());
	}
	
	void Update() {
		if(!GameManager.ins.playing) return;
		
		sm.Update();
	}

	void FixedUpdate() {
	}

	public void RequestHold(Holdable request) {
		if(!GameManager.ins.playing) return;

		if(sm.current != null && sm.current.GetType() == typeof(NotHoldingState)) {
			sm.Next(new HoldingState(request));
		}
	}

	public void Die() {
		GameManager.ins.sm.Next(new GameManager.LoseState());
	}

	// ==============================================================| STATE |================================================================================= //
	public class NotHoldingState : State<Player> {
		public override void OnStay() {
			RaycastHit hit;
			Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit,pt.interactionDistance,pt.interactionLayer.value);
			if(hit.collider != null) {
				Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
				pt.interactCrosshairActive = interactable.playerInteractable;
				if(interactable.playerInteractable && Input.GetMouseButtonDown(0)) {
					interactable.Use(pt);
				}
			} else {
				pt.interactCrosshairActive = false;
			}
		}
	}
	// ==============================================================| STATE |================================================================================= //
	public class HoldingState : State<Player> {
		private Holdable holdable;
		public HoldingState(Holdable holdable) {
			this.holdable = holdable;
		}
		public override void OnEnter() {
			holdable.OnStartHold(Camera.main.transform);
			holdable.transform.position = pt.holdableTarget.position;			
			pt.interactCrosshairActive = true;
		}

		public override void OnStay() {
			Vector3 delta = (pt.holdableTarget.position - holdable.transform.position);
			holdable.GetComponent<Rigidbody>().velocity = delta.normalized * Mathf.Pow(delta.magnitude,2) * pt.holdableSnapSpeed;
			// holdable.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			if(delta.magnitude > pt.maxHoldableDistance || Input.GetMouseButtonDown(0)) {
				pt.sm.Next(new NotHoldingState());
			}
		}

		public override void OnExit() {
			holdable.OnEndHold();
		}
	}
}
