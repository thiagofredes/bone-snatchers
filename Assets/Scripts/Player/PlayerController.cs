using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, IDamaging, IDamageable
{

	public static Transform PlayerTransform {
		get {
			return _instance.transform;
		}
	}

	public static bool PlayerDead {
		get {
			return _instance.Dead ();
		}
	}

	public Animator animator;

	public CharacterController characterController;

	public Collider playerCollider;

	public float movementSpeed;

	public MoveHitboxController hitboxController;

	public ComboController comboController;

	private PlayerState state;

	private static PlayerController _instance;

	public float health = 20f;

	private AudioSource source;

	private float maxHealth;

	private bool gameEnd;

	void Awake ()
	{
		_instance = this;
		source = GetComponent<AudioSource> ();
		maxHealth = health;
		gameEnd = false;
	}

	private void OnGameEnd ()
	{
		gameEnd = true;
	}

	void Start ()
	{
		state = new PlayerRunning (this);	
	}

	void Update ()
	{
		if (!gameEnd)
			state.Update ();
	}

	public void SetState (PlayerState state)
	{
		this.enabled = false;
		this.state.OnExit ();
		this.state = state;
		this.state.OnEnter ();
		this.enabled = true;
	}

	public void Damage (IDamageable other)
	{
		other.Damage (5f);
	}

	public void Damage (float damage)
	{
		this.health = Mathf.Clamp (this.health - damage, 0f, maxHealth);
		this.state.Reset ();
		this.animator.SetTrigger ("GotHit");
		if (this.health <= 0) {			
			source.volume = 1f;
			this.animator.SetTrigger ("Die");
		}
	}

	public bool Dead ()
	{
		if (this.health <= 0) {
			return true;
		}
		return false;
	}

	public void AddHealth (float healthUp)
	{
		this.health = Mathf.Clamp (this.health + healthUp, 0f, maxHealth);
	}

}
