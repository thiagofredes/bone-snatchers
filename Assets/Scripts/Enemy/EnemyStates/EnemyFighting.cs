using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class EnemyFighting : EnemyState
{

	private EnemyController enemy;

	private bool performingCombo = false;

	private float nextTimeToAttack;


	public EnemyFighting (EnemyController enemy)
	{
		this.enemy = enemy;
		performingCombo = false;
		nextTimeToAttack = Time.time + this.enemy.timeToAttack;
	}

	public override void Reset ()
	{
		performingCombo = false;
		nextTimeToAttack = Time.time + this.enemy.timeToAttack;
	}

	public override void Update ()
	{
		this.enemy.LookAtPlayer ();
		if (Vector3.Distance (PlayerController.PlayerTransform.position, enemy.transform.position) > enemy.fightingDistance) {
			Reset ();
			enemy.SetState (new Chasing (this.enemy));
		}
		if (Time.time >= nextTimeToAttack && !performingCombo) {
			enemy.comboController.Attack ();
			performingCombo = true;
		}
	}

	public override void OnEnter ()
	{
		enemy.comboController.ComboEnded += OnComboEnded;
		enemy.comboController.WindowOpened += OnWindowOpened;
	}

	public override void OnExit ()
	{
		enemy.comboController.ComboEnded -= OnComboEnded;
		enemy.comboController.WindowOpened -= OnWindowOpened;
	}

	private void OnComboEnded ()
	{
		Reset ();
	}

	private void OnWindowOpened ()
	{
		enemy.comboController.Attack ();
	}
}

