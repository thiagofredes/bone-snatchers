using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class ComboController : MonoBehaviour
{

	public event Action CanAttack;

	public event Action WindowLost;

	public event Action ComboEnded;

	public event Action WindowOpened;

	public string[] animationTriggers;

	public string missedWindowAnimationTrigger;

	public HitboxController hitboxController;

	public Animator animator;

	private int currentTrigger = 0;

	private bool openWindow;

	private bool windowReached;

	private bool canAttack;

	private int numTriggers;


	public void Awake ()
	{
		numTriggers = animationTriggers.Length;
		Reset ();
	}

	public void Reset ()
	{
		currentTrigger = 0;
		openWindow = false;
		windowReached = false;
		canAttack = false;
		hitboxController.DisableAllHitboxes ();
	}

	public void Attack ()
	{
		if (currentTrigger == 0) {
			animator.SetTrigger (animationTriggers [currentTrigger++]);
		} else {
			if (openWindow) {
				windowReached = true;
			}
		}
	}

	public void AnimationFrameWindowOpened ()
	{
		if (!openWindow) {
			openWindow = true;
			windowReached = false;
			if (WindowOpened != null) {
				WindowOpened ();
			}
		}
	}

	public void HitFrameOpen ()
	{
		hitboxController.EnableHitbox (animationTriggers [currentTrigger - 1]);
	}

	public void HitFrameClosed ()
	{
		hitboxController.DisableHitbox (animationTriggers [currentTrigger - 1]);
	}

	public void AnimationFrameWindowClosed ()
	{
		if (openWindow) {
			openWindow = false;
			if (!windowReached) {				
				if (WindowLost != null) {
					WindowLost ();
				}
			}
		}
	}

	public void CanAttackAgain ()
	{
		if (windowReached) {
			if (currentTrigger < numTriggers) {
				animator.SetTrigger (animationTriggers [currentTrigger++]);
				openWindow = false;
				windowReached = false;
				if (CanAttack != null)
					CanAttack ();
			} else {
				animator.SetTrigger (missedWindowAnimationTrigger);
				Reset ();
				if (ComboEnded != null)
					ComboEnded ();
			}
		} else {
			animator.SetTrigger (missedWindowAnimationTrigger);
			Reset ();
			if (ComboEnded != null)
				ComboEnded ();
		}
	}
}
