using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	public bool playerInteractable = true;
	public virtual string GetName() {
		return name;
	}
	public virtual string GetDescription() {
		return "Interact with " + GetName();
	}
	public virtual void Use(Player player) {

	}
}
