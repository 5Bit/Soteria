using UnityEngine;
using System.Collections;

public class CameraAtCharacter : MonoBehaviour {

	public GameObject character;
	public Camera cam;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		cam.transform.LookAt(character.transform.position);
	
	}
}
