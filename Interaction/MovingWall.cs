using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovingWall : Interactable {
	public CinemachineDollyCart cart;
	public float speed = 5f;

	private bool moving = false;

	public override void Use(Player player) {
		if(moving) return;

		StartCoroutine(Move());
	}

	IEnumerator Move() {
		bool originalPlayerInteractable = playerInteractable;
		moving = true;
		playerInteractable = false;

		float direction = cart.m_Position < 0.5f ? 1f : -1f;
		float initialPosition = cart.m_Position;
		while(Mathf.Abs(cart.m_Position - initialPosition) < 0.99f) {
			cart.m_Position += direction * speed * Time.deltaTime;
			yield return null;
		}

		cart.m_Position = Mathf.Round(cart.m_Position);
		moving = false;
		playerInteractable = originalPlayerInteractable;

		yield return null;
	}
}
