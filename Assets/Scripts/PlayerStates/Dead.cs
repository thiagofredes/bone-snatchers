using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : PlayerState
{

	private PlayerController player;

	public Dead (PlayerController enemy)
	{
		this.player = enemy;
		this.player.characterController.enabled = false;
		this.player.playerCollider.enabled = false;
		this.player.animator.SetTrigger ("Die");
	}

	public override void Reset ()
	{
		throw new System.NotImplementedException ();
	}

	public override void Update ()
	{

	}

	public override void OnEnter ()
	{
	}

	public override void OnExit ()
	{
	}
}
