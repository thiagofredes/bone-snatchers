using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : MonoBehaviour, IDamageable, IDamaging
{
	public static event Action EnemyDead;

	public NavMeshAgent navMeshAgent;

	public Collider enemyCollider;

	public Animator animator;

	public ComboController comboController;

	public float chasingSpeed = 8f;

	public float chasingAcceleration = 10f;

	public MoveHitboxController hitboxController;

	public float timeToAttack = 5f;

	public float damage;

	public float health = 20f;

	private PlayerController player;

	private EnemyState currentState;

	private AudioSource source;


	void Awake ()
	{
		player = FindObjectOfType<PlayerController> ();
		source = GetComponent<AudioSource> ();
	}

	void Start ()
	{
		SetState (new EnemyFighting (this));
	}

	// Update is called once per frame
	void Update ()
	{
		currentState.Update ();
	}

	public void SetState (EnemyState state)
	{
		this.enabled = false;
		if (this.currentState != null)
			this.currentState.OnExit ();
		this.currentState = state;
		this.currentState.OnEnter ();
		this.enabled = true;
	}

	public void Damage (float damage)
	{		
		this.health -= damage;
		this.currentState.Reset ();
		this.animator.SetTrigger ("GotHit");
		if (this.health <= 0) {
			//SWAP FOR NEW ENEMY DEAD
			this.source.Play ();
			EnemyController.EnemyDead ();
		}
	}

	public void Damage (IDamageable other)
	{
		other.Damage (this.damage);
	}

	public void LookAtPlayer ()
	{
		Vector3 lookVector = player.transform.position - this.transform.position;
		lookVector.y = 0;
		lookVector.Normalize ();
		this.transform.rotation = Quaternion.LookRotation (lookVector);
	}
}
