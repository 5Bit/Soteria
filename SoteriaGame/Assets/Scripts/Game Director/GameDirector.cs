﻿using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {

//	private Player _player;

	protected static GameDirector _instance;
	
#region Managers

	private AudioManager _audioManager;
	private InputManager _inputManager;

#endregion

	public static GameDirector instance
	{
		get {
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<GameDirector>();
			return (GameDirector)(_instance);
		}
	}

	// Use this for initialization
	private void Awake () 
	{
		if(_instance == null)
		{
			_instance = this;
			this.InitializeManagers();
			DontDestroyOnLoad(this); //Keep the instance going between scenes
		}
		else
		{
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	private void  Update () {
	
		_inputManager._Update();
		_audioManager._Update(); //This will change, We'll use events rather than Updates.

	}
	private void InitializeManagers()
	{
		//This is problematic (AddComponent)-> it forces the script to be a component and uses the 
		// Update function automatically each frame, only solution not use MonoBehavior <-- not so simple

		_audioManager = this.gameObject.AddComponent<AudioManager>();
		_audioManager.Initialize();
		_inputManager = this.gameObject.AddComponent<InputManager>();
		_inputManager.Initialize();
	}
}
