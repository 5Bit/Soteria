﻿using UnityEngine;
using System.Collections;

public class SafetyLightController : MonoBehaviour {

    public Transform player;
	GameObject safeArea;

	public enum State
	{
		Finding,
		Moving
	};
	State currentState;

	float lightSpeed = 2.0f;

	// Use this for initialization
	void Start () {
		currentState = State.Finding;	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState == State.Finding) 
		{
			FindNextClosest ();	
		}

		if (currentState == State.Moving) 
		{
            
			this.transform.position = Vector3.MoveTowards(this.transform.position, safeArea.transform.position, Time.deltaTime * lightSpeed);
			if (Vector3.Distance(this.transform.position, player.transform.position) >= 15.0f)
			{
                Vector3 normal = player.forward;
                //Debug.Log ("Player pos: " + player.position);
                //Debug.Log ("Player normal: " + player.forward);
                normal.Normalize();
                //Debug.Log ("Normalized: " + normal);
                this.transform.position = new Vector3(5 * normal.x + player.position.x, this.transform.position.y, 5 * normal.z + player.position.z);
                if (!player.GetComponent<EncounterMovementController>().enabled)
                {
                    this.light.enabled = false;
                }
			}
            if (Vector3.Distance(this.transform.position, safeArea.transform.position) <= 1.0f)
            {
                this.light.enabled = false;
            }
		}
	}

	void FindNextClosest()
	{
		safeArea = GameObject.FindWithTag ("SafeArea");
		currentState = State.Moving;
	}
}
