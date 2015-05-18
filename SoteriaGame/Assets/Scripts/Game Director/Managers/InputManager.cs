﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
	private List<Command> _buttonTypes = new List<Command>();
	private List<Button> _input = new List<Button>();
	
	private Timer _inputTimer;
	private TimersType _timertype;
	private bool _isqtemode;
	
	public static int QTE_Delay = 3;
	
	// Use this for initialization
	void Awake () 
	{
		this.enabled = false;
		this._timertype = TimersType.Input;
		this._isqtemode = false;

		//CreateButtonList//
		_buttonTypes.Add( 	new LeftCommand()  ); //LeftArrow
		_buttonTypes.Add( 	new RightCommnad() ); //RightArrow
		_buttonTypes.Add( 	new UpCommand()    ); //UpArrow
		_buttonTypes.Add(  	new DownCommand()  ); //DownArrow
		_buttonTypes.Add(   new SpaceCommand() ); //Spacebar
		
	}
	
	// Update is called once per frame
	public void Update () 
	{
		if (GameDirector.instance.GetCurrentGameState() == GameStates.Encounter)
			_isqtemode = true;
		else
			_isqtemode = false;	



		if (_isqtemode == true)
		{
			this.ProcessQTEInput();

			if (this._inputTimer.ElapsedTime() >= QTE_Delay)
			{
				for (int i = 0; i < this._input.Count; i++)
				{
					if (this._input[i].Killit())
					{
						this._input.RemoveAt(i);
						break;
					}
				}
				
				if (_input.Count == 0)
					this._inputTimer.ResetTimer();
			}
		}
		else
		{
			this.ProcessInput();
			
			this.PurgeInputList();
		}
	}
	
	public int getPressCount()
	{
		return _input.Count;
	}

	private void ProcessQTEInput()
	{	
		bool inputpressed = false;

		if (Input.GetKeyDown(KeyCode.Space))
			inputpressed = executeInternalInput((int)ButtonType.SpaceBar, (Object) GameDirector.instance.GetPlayer() );

		if (inputpressed)
			this._inputTimer.StartTimer ();
	}
	private void ProcessInput()
	{
		ButtonType buttonType = ButtonType.None;

		if (Input.GetKey( KeyCode.UpArrow))
			buttonType = ButtonType.UpArrow;
		if (Input.GetKey( KeyCode.DownArrow))
			buttonType = ButtonType.DownArrow;
		if (Input.GetKey( KeyCode.LeftArrow))
			buttonType = ButtonType.LeftArrow;
		if (Input.GetKey( KeyCode.RightArrow))
			buttonType = ButtonType.RightArrow;
	
		if (buttonType != ButtonType.None)
			executeInternalInput((int)buttonType, (Object) GameDirector.instance.GetPlayer() );
	}
	
	public void executeExternalInput(int inButtonType, Object inActor = null)
	{
		executeExternalInput(inButtonType, inActor);
	}
	
	private bool executeInternalInput(int inButtonType, Object inActor = null)
	{
		return executeInput(inButtonType, inActor);
	}
	
	private bool executeInput(int inButtonType, Object inActor = null)
	{
		Button Temp = new Button( (ButtonType)inButtonType, _buttonTypes[inButtonType], Time.time);
		Temp.execute(inActor);

		if (this._isqtemode)
			this.AddToInputList( Temp );
		
		return true;
	}
	
	private void PurgeInputList()
	{
		if (this._input.Count > 0)
		{
			this._input.Clear();	
			this._inputTimer.StopTimer();
		}
	}
	
	private void AddToInputList(Button inButton)
	{
		this._inputTimer.ResetTimer();
		this._input.Add(inButton);
	}
	
	public void Initialize()
	{
		this._inputTimer = TimerManager.instance.Attach(this._timertype);
	}

	public bool isQTEMode()
	{
		return this._isqtemode;
	}
}