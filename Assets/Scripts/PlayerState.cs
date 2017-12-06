using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
	public abstract void Reset ();

	public abstract void Update ();

	public abstract void OnEnter ();

	public abstract void OnExit ();
}
