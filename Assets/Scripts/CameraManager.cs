using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager 
{
	public ArrayList cameras;
	private ArrayList camControllers;
	private Camera active;
	private SortedList allActiveCams;

	public CameraManager(GameObject firstCam) {
		cameras = new ArrayList();
		cameras.Add(firstCam);
		camControllers = new ArrayList();
		camControllers.Add(firstCam.GetComponent<CameraController>());
		active = firstCam.GetComponent<Camera>();
		allActiveCams = new SortedList();
		
		allActiveCams.Add(0.0f, active);
		SetActive(0.0f,firstCam);
	}

	public void MoveCamerasToTime(float time)
	{
		foreach (CameraController cc in camControllers)
		{
			cc.SetAtTime(time);
		}
		
	}

	public void AddCamera(GameObject camGO)
	{
		Camera cam = camGO.GetComponent<Camera>();
		cameras.Add(cam);
		camControllers.Add(camGO.GetComponent<CameraController>());
		if (cameras.Count == 0)
		{
			SetActive(0, camGO);
		}
		else if (cameras.Count > 7)
		{
			throw new UnityException("yo you cant have more than 7 of these");
		}
		else
		{
			cam.targetDisplay = cameras.Count-1;
		}
	}

	public void RemoveCamera(GameObject camGO)
	{
		cameras.Remove(camGO.GetComponent<Camera>());
		camControllers.Remove(camGO.GetComponent<CameraController>());
		int idx = allActiveCams.IndexOfValue(camGO.GetComponent<CameraController>());
		if(idx != -1)
			allActiveCams.RemoveAt(idx);
		//camGO.SetActive(false);
	}

	public void SetActive(float time, GameObject camGO)
	{
		Camera cam = camGO.GetComponent<Camera>();
		if (!active.Equals(cam)) {
		
		active.enabled = false;
		cam.enabled = true;
		active = cam;
		allActiveCams.Add(time, cam);
		}
	}

	public CameraController GetActive(float time)
	{
		int idx = 0;
		CameraController active = null;
		foreach (float t in allActiveCams.Keys)
		{
			if(time >= t)
			{
				active = ((Camera) allActiveCams.GetByIndex(idx))
					.gameObject.GetComponent<CameraController>();
				idx++;
			} else { 
				break;
			}
		}
		if (active == null)
			return (CameraController)camControllers[camControllers.Count-1];
		return active;
	}

	public void RunThroughScene(float time)
	{
		float lastT = 0;
		int idx = 0;
		MoveCamerasToTime(time);
		foreach(float t in allActiveCams.Keys)
		{
			if(time >= lastT && time < t)
			{
				Camera active = (Camera) allActiveCams.GetByIndex(idx);
				active.enabled = true;
				if(idx > 0)
				{
					Camera notYou = (Camera) allActiveCams.GetByIndex(idx-1);
					notYou.enabled = false;
				}
				break;
			}
			
		}
	}
}
