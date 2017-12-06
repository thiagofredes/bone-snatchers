using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighting : PlayerState
{
	private PlayerController player;

	private bool canChangeLookDirection;

	public PlayerFighting (PlayerController player)
	{
		this.player = player;
		canChangeLookDirection = false;
		player.comboController.Attack ();
	}

	public override void OnEnter ()
	{
		this.player.comboController.CanAttack += CanAttackAgain;
		this.player.comboController.WindowLost += OnWindowLost;
		this.player.comboController.ComboEnded += OnWindowLost;
	}

	public override void OnExit ()
	{
		this.player.comboController.CanAttack -= CanAttackAgain;
		this.player.comboController.WindowLost -= OnWindowLost;
		this.player.comboController.ComboEnded -= OnWindowLost;
	}

	private void CanAttackAgain ()
	{
		canChangeLookDirection = true;
	}

	private void OnWindowLost ()
	{
		this.player.SetState (new PlayerRunning (this.player));
	}

	public override void Reset ()
	{
		this.canChangeLookDirection = false;
		this.player.SetState (new PlayerRunning (this.player));
	}

	public override void Update ()
	{	
		if (canChangeLookDirection) {
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");
			Vector3 lookDirection = ThirdPersonCameraController.CameraForwardProjectionOnGround * vertical + ThirdPersonCameraController.CameraRightProjectionOnGround * horizontal;

			if (lookDirection.magnitude == 0.0f) {
				player.transform.rotation = Quaternion.LookRotation (ThirdPersonCameraController.CameraForwardProjectionOnGround);
			} else {
				player.transform.rotation = Quaternion.LookRotation (lookDirection);
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			canChangeLookDirection = false;
			player.comboController.Attack ();
		}
	}
}
