using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RVRControllerManager : MonoBehaviour 
{

	public GameObject laserPrefab;
	private GameObject laser;
	public GameObject cameraPrefab;
	public Transform controllerTransform;
	public Transform headTransform;

	public GameObject editingCamera;
	private bool justOpenedOptions;
	private bool contKfRecord;

	private Vector3 gripDwnLoc;
	private Vector3 headLoc;
	private float distToHead;
	private float currAnimTime;
	private float currRealTime;

	public SceneController sceneController;
	public UIController uiController;
	private CameraManager camManager;
	
	private float timeDiff;

	private float sign;
	private bool playingAnim;

	private Vector2 tchDwnPos;
	private Vector2 tchUpPos;
	System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
	System.Diagnostics.Stopwatch menuStopwatch = new System.Diagnostics.Stopwatch();


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

	[SteamVR_DefaultAction("InteractUI")]
	public SteamVR_Action_Boolean menu_trigger;

	// Use this for initialization
	void Start () 
	{
		laser = Instantiate(laserPrefab);
		laser.transform.position = controllerTransform.position;
		laser.transform.rotation = controllerTransform.rotation;
		camManager = new CameraManager(editingCamera);
		playingAnim = false;
		contKfRecord = false;
		timeDiff = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currRealTime = sceneController.GetCurrentTime();
		laser.transform.position = controllerTransform.position;
		laser.transform.rotation = controllerTransform.rotation;

		SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;

		float rightTrig = a_trigger.GetAxis(rightHand);
		bool joyPressed = joy_btn.GetState(rightHand);
		bool joyPressedDwn = joy_btn.GetStateDown(rightHand);
		bool joyTouched = joy_tch.GetState(rightHand); 
		bool joyTchUp = joy_tch.GetLastStateUp(rightHand);
		bool joyTchDwn = joy_tch.GetLastStateDown(rightHand);
		Vector2 rightTrackPos = scroll_trigger.GetLastAxis(rightHand);
		bool rightHandle = grab_trigger.GetState(rightHand);
		bool rightHandleDwn = grab_trigger.GetStateDown(rightHand);
		bool rightHandleUp = grab_trigger.GetStateUp(rightHand);
		bool rightClickDwn = click_trigger.GetStateDown(rightHand);
		bool rightClick = click_trigger.GetState(rightHand);
		bool rightClickUp = click_trigger.GetStateUp(rightHand);
		bool trigClickUp = click_trigger.GetLastStateUp(rightHand);
		bool menuClick = menu_trigger.GetState(rightHand);
		bool menuDown = menu_trigger.GetStateDown(rightHand);
		bool menuUp = menu_trigger.GetStateUp(rightHand);
		

		if(!rightClick) {
			camManager.GetActive(currRealTime)
								 .SetAtTime(currRealTime);
		}

		if(rightClick && timeDiff > 2)
		{
			// show options
			justOpenedOptions = true;
		}

		if(trigClickUp)
		{
			if(!justOpenedOptions)
			{
				RaycastHit hit;
				if (Physics.Raycast(controllerTransform.position + new Vector3(0.01f, 0, 0),
					 transform.forward, out hit, 100))
				{
					Vector3 hitPoint = hit.point;
					bool isCamera = hit.transform.gameObject.CompareTag("camera");
					if(isCamera)
					{
						camManager.SetActive(currRealTime,
											 hit.transform.gameObject);
					}
				}
			}
			justOpenedOptions = false;
		}

		if(rightHandleDwn)
		{
			gripDwnLoc = controllerTransform.position;
			headLoc = headTransform.position;
			distToHead = Vector3.Distance(headLoc, gripDwnLoc);

			currAnimTime = currRealTime;

		}
		if(rightHandle)
		{
			Debug.Log("Rewind/ff'd");
			float delta = Vector3.Distance(gripDwnLoc, controllerTransform.position);
			float deltaDistToHead = 
				Vector3.Distance(controllerTransform.position, headTransform.position) - distToHead;
			sign = Mathf.Sign(deltaDistToHead);
			float spd = 1; // to be tested later
			delta *= spd * sign;
			if (currAnimTime + delta >= 0) {
				sceneController.JumpToTime(currAnimTime + delta);
			}
			else {
				sceneController.JumpToTime(0);
			}

		}
		if (rightHandleUp) 
		{
			sceneController.Pause();
		}


		// Keyframe recording
		if(rightClickDwn)
		{
			stopwatch.Start();
			KeyframeSet camKfs = camManager.GetActive(currRealTime).keyframeSet;
			Keyframe kf = new Keyframe(transform.position, transform.rotation);
			camKfs.AddKeyframe(currRealTime, kf);
			// editingCamera.transform.position = transform.position;
			// editingCamera.transform.rotation = transform.rotation;
		}
		else if(rightClick && timeDiff > 2)
		{
			contKfRecord = true;
			sceneController.Play();
		}
		else if(rightClickUp)
		{
			stopwatch.Stop();
			stopwatch.Reset();
			contKfRecord = false;
			sceneController.Pause();
		}
		else
		{
			timeDiff = stopwatch.ElapsedMilliseconds/1000;
		}

		// Keyframe snapping
		if(joyTchDwn)
		{
			tchDwnPos = rightTrackPos;
		}
		// swiped right
		else if(joyTchUp && (rightTrackPos-tchDwnPos).x > 0.7)
		{
			Debug.Log("Fast Forwarded");
			float currAnimTime = currRealTime;
			float kfTime = camManager.GetActive(currRealTime)
				.keyframeSet.GetNextTime(currAnimTime);
			camManager.GetActive(currAnimTime).SetAtTime(kfTime);
			sceneController.JumpToTime(kfTime);
		}
		// swiped left
		else if(joyTchUp && (rightTrackPos-tchDwnPos).x < -0.7)
		{
			Debug.Log("Rewinded");
			float currAnimTime = currRealTime;
			float kfTime = camManager.GetActive(currRealTime)
				.keyframeSet.GetPreviousTime(currAnimTime);
			camManager.GetActive(currAnimTime).SetAtTime(kfTime);
			sceneController.JumpToTime(kfTime);
		}

		// create and remove cameras
		if (menuDown) 
		{
			menuStopwatch.Start();
		}
		else if (menuUp) 
		{
			if (menuStopwatch.ElapsedMilliseconds > 2000) 
			{
				// remove camera
				RaycastHit hit;
				if (Physics.Raycast(controllerTransform.position + new Vector3(0.01f, 0, 0),
					 transform.forward, out hit, 100))
				{
					Debug.Log(hit.transform.gameObject);
					Vector3 hitPoint = hit.point;
					bool isCamera = hit.transform.gameObject.CompareTag("camera");
					//hit.transform.gameObject.tag
					if(isCamera)
					{
						// remove from world
						Debug.Log("hhhh");
						camManager.RemoveCamera(hit.transform.gameObject);
						Destroy(hit.transform.gameObject);
						camManager.SetActive(currRealTime, 
							((Camera)camManager.cameras[camManager.cameras.Count-1]).gameObject);
					}
				}
			}
			else if (!camManager.GetActive(currRealTime).keyframeSet.keyframes.ContainsKey(currRealTime)) 
			{
				KeyframeSet camKfs = camManager.GetActive(currRealTime).keyframeSet;
				Keyframe kf = new Keyframe(transform.position, transform.rotation);
				camKfs.AddKeyframe(currRealTime, kf);
				camManager.GetActive(currRealTime).SetAtTime(currRealTime);

				GameObject camera = Instantiate(cameraPrefab,transform.position,
												transform.rotation);
				camManager.AddCamera(camera);
				camManager.SetActive(currRealTime, camera);
			}
			menuStopwatch.Stop();
		}
	}

	// called 30 frames/sec
	void FixedUpdate()
	{
		if(contKfRecord)
		{
			KeyframeSet camKfs = camManager.GetActive(currRealTime).keyframeSet;
			Keyframe kf = new Keyframe(transform.position, transform.rotation);
			camKfs.AddKeyframe(currRealTime, kf);
			camManager.GetActive(currRealTime).gameObject.transform.position = 
				transform.position;
			camManager.GetActive(currRealTime).gameObject.transform.rotation = 
				transform.rotation;
		}
	}
}
