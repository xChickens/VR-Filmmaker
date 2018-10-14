using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public KeyframeSet keyframeSet;
    private bool isActive;
    public Camera cam;
    public Transform initTransform;
    public SceneController sceneController;

	// Use this for initialization
	void Start ()
    {
        keyframeSet = new KeyframeSet();
		transform.position = initTransform.position;
        transform.rotation = initTransform.rotation;
        Keyframe kf = new Keyframe(transform.position, transform.rotation);
        isActive=true;
            if (sceneController.Equals(null)) {
            keyframeSet.AddKeyframe(0, kf);
        }
        else {
        keyframeSet.AddKeyframe(sceneController.GetCurrentTime(), kf);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(this.isActive) 
        {
            cam.enabled = true;
        }
        else 
        {
            cam.enabled = false;
        }
	}
    
    public void init(){
        Start();
    }

    public void SetAtTime(float time)
    {
        Keyframe kf;
        if (!keyframeSet.keyframes.ContainsKey(time)) {
            kf = keyframeSet.GetInterpolatedKeyframe(time);
        }
        else {
            kf = keyframeSet.GetKeyframe(time);
        }
        
        transform.position = kf.pos;
        transform.rotation = kf.rot;
    }
}
