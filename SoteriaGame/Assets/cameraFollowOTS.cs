using UnityEngine;
using System.Collections;

public class cameraFollowOTS : MonoBehaviour {

	public Transform target;
	public float smooth;
	public Vector3 leftBankCameraOffset;
	public int crossBridge; // 0 == on left
							// 1 == on birdge
							// 2 == on right

	public Quaternion leftBankCameraRotation;
	public Quaternion bridgeCameraRotation;
	public Quaternion rightBankCameraRotation;

	public float cameraLocalMovement; // Used to move camera from left to right during transitions.




	// Use this for initialization
	void Start () {
		if (target == null)
			target = GameObject.FindWithTag("Player").transform;
		crossBridge = 0;

		leftBankCameraOffset = this.transform.position - target.position;
		leftBankCameraRotation = this.transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		if(crossBridge == 0)
			followleftBank();
		if(crossBridge == 1)
			followBridge();
		if(crossBridge == 2)
			followRightBank();


	

	}

	void followleftBank()
	{
		this.transform.position = new Vector3(target.position.x + leftBankCameraOffset.x, target.position.y + leftBankCameraOffset.y, target.position.z + leftBankCameraOffset.z);
		this.transform.rotation = leftBankCameraRotation;
	}

	void followBridge()
	{
		this.transform.position = new Vector3(target.position.x + leftBankCameraOffset.x + cameraLocalMovement, target.position.y + leftBankCameraOffset.y, target.position.z + leftBankCameraOffset.z);
		this.transform.rotation = bridgeCameraRotation;
	}

	void followRightBank()
	{
		this.transform.position = new Vector3(target.position.x + leftBankCameraOffset.x + (2*cameraLocalMovement), target.position.y + leftBankCameraOffset.y, target.position.z + leftBankCameraOffset.z);
		//rightBankCameraRotation = this.transform.rotation;
		this.transform.rotation = rightBankCameraRotation;
	}
}
