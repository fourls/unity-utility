using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable {
	public List<Interactable> linked;
	public override void Use(Player player) {
		GetComponent<Animator>().SetTrigger("Used");
		foreach(Interactable interactable in linked) {
			interactable.Use(null);
		}
	}
}
