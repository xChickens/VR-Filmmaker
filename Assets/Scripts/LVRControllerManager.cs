using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LVRControllerManager : MonoBehaviour 
{	
	enum Mode {View, Edit};
	public Animator anim;
	public GameObject laserPrefab;
	private GameObject laser;
	public GameObject targetReticlePrefab;
	private GameObject targetReticle;
	public Vector3 targetReticleOffset;
	public Transform controllerTransform;
	private Vector3 hitPoint;
	public Transform headTransform;
	public Transform parentTransform;
	private Mode mode;

	public SceneController sceneController;
	public CameraController camController;
	public UIController uiController;
	private bool playingAnim;

	[SteamVR_DefaultAction("GrabPinch")]
	public SteamVR_Action_Boolean joy_btn;

	[SteamVR_DefaultAction("JoyTouch")]
	public SteamVR_Action_Boolean joy_tch;

	[SteamVR_DefaultAction("squeeze")]
    public SteamVR_Action_Single a_trigger;

	[SteamVR_DefaultAction("Teleport")]
	public SteamVR_Action_Boolean click_trigger;

	[SteamVR_DefaultAction("scroll")]
	public SteamVR_Action_Vector2 scroll_trigger;

	[SteamVR_DefaultAction("GrabGrip")]
	public SteamVR_Action_Boolean grab_trigger;

	// Use this for initialization
	void Start () 
	{
		laser = Instantiate(laserPrefab);
		laser.transform.position = controllerTransform.position;
		laser.transform.rotation = controllerTransform.rotation;
		
		targetReticle = Instantiate(targetReticlePrefab);	
		targetReticle.SetActive(false);
		playingAnim = false;
		//anim.Play("Default Take");
		//anim.Play("ArmatureAction");
		//anim.Play("Armature_001Action");
		//anim.Play("Base_001Action");
		//anim.Play("HeadArmature_001Action");
		//anim.Play("LeftShoulderArm_001Action");
		//anim.Play("RightShoulderArm_001Action");
		//anim.Play("LeftHandArm_001Action");
		//anim.Play("RightHandArm_001Action");
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		laser.transform.position = controllerTransform.position;
		laser.transform.rotation = controllerTransform.rotation;

		SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;

		// switch modes?
		float leftTrig = a_trigger.GetAxis(leftHand);
		bool joyPressed = joy_btn.GetState(leftHand);
		bool joyPressedDwn = joy_btn.GetStateDown(leftHand);
		bool joyTouched = joy_tch.GetState(leftHand); 
		bool joyTchUp = joy_tch.GetLastStateUp(leftHand);
		bool joyTchDwn = joy_tch.GetLastStateDown(leftHand);
		Vector2 leftTrackPos = scroll_trigger.GetLastAxis(leftHand);
		bool leftHandle = grab_trigger.GetState(leftHand);
		bool leftClick = click_trigger.GetState(leftHand);
		bool leftClickDwn = click_trigger.GetStateDown(leftHand);

		RaycastHit hit;
		bool canTransport = false;
		targetReticle.SetActive(false);
		if (Physics.Raycast(transform.position + new Vector3(0.01f, 0, 0), 
				transform.forward, out hit, 300))
			{
				hitPoint = hit.point;
				// Debug.Log(hit.transform.gameObject);
				//Debug.Log(controllerTransform.position);
				canTransport = hit.transform.gameObject.CompareTag("transportable");
				if(canTransport)
				{
					//Debug.Log("transporting");
					targetReticle.transform.position = hitPoint + targetReticleOffset;
					targetReticle.SetActive(true);
				}
			}

		// teleport if possible
		if (leftClickDwn && canTransport)
		{
			//Transform parentTrans = GetComponentInParent<Transform>();
			Vector3 difference = parentTransform.position - headTransform.position;
    		difference.y = 0;
			parentTransform.position = hitPoint + difference;
		}

		// pause/play
		if(joyPressedDwn)
		{
			if(playingAnim)
			{
				sceneController.Pause();
				playingAnim = false;
			}
			else
			{
				sceneController.Play();
				playingAnim = true;
			}
		}
		
	}
}
