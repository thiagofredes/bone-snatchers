﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunning : PlayerState
{

	private PlayerController player;


	public PlayerRunning (PlayerController player)
	{
		this.player = player;
	}

	public override void Reset ()
	{
		
	}

	public override void Update ()
	{		
		if (!player.Dead ()) {
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");
			Vector3 movement = ThirdPersonCameraController.CameraForwardProjectionOnGround * vertical + ThirdPersonCameraController.CameraRightProjectionOnGround * horizontal;

			if (Input.GetKey (KeyCode.Space)) {			
				this.player.SetState (new PlayerFighting (this.player));
			}

			if (movement.magnitude < 0.1f)
				player.transform.rotation = Quaternion.LookRotation (player.transform.forward);
			else
				player.transform.rotation = Quaternion.LookRotation (movement);

			player.animator.SetFloat ("Forward", movement.normalized.magnitude);
			player.characterController.Move (player.movementSpeed * Time.deltaTime * (movement - 3 * Vector3.up).normalized);
		}
	}

	public override void OnEnter ()
	{
		
	}

	public override void OnExit ()
	{
		
	}
}
