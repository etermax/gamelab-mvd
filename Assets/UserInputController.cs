using System.Collections;
using System.Collections.Generic;
using Presentation.Game;
using UnityEngine;

public class UserInputController : MonoBehaviour
{

	private GameController gameController;
	
	// Use this for initialization
	void Awake ()
	{
		gameController = GetComponent<GameController>();
	}
	
	void Update () {
		if (Input.GetButtonDown("Fire2"))
		{
			gameController.OnButton2Pressed();
		}
			
	}
}
