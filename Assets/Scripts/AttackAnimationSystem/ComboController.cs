using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComboController : MonoBehaviour
{

	public event Action CanAttack;

	public event Action WindowLost;

	public event Action ComboEnded;

	public string[] animationTriggers;

	public string missedWindowAnimationTrigger;

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
		}
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
