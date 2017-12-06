using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCapture : MonoBehaviour
{

	public CursorLockMode lockMode;

	public bool visible;

	public bool applyOnStart = false;


	void Start ()
	{
		if (applyOnStart) {
			Cursor.visible = visible;
			Cursor.lockState = lockMode;
		}
	}

	public void Apply ()
	{
		Cursor.visible = visible;
		Cursor.lockState = lockMode;
	}
}
