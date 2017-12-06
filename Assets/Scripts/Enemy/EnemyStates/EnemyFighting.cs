using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

