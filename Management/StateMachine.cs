using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> {
	private T owner;
	private State<T> currentState = null;

	public StateMachine(T owner) {
		this.owner = owner;
	}

	public void Next(State<T> newState) {
		if(currentState != null) currentState.OnExit();

		currentState = newState;

		if(currentState != null) {
			currentState.owner = owner;
			currentState.OnEnter();
		}
	}

	public void Update() {
		if(currentState != null) currentState.OnStay();
	}

}

public class State<T> {
	public T owner;

	public virtual void OnEnter() {

	}

	public virtual void OnStay() {

	}

	public virtual void OnExit() {

	}
}
