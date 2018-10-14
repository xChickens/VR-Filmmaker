using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Keyframe
{
    public Vector3 pos;
    public Quaternion rot;

    public Keyframe(Vector3 pos, Quaternion rot)
    {
        this.pos = pos;
        this.rot = rot;
    }
}

public struct KeyframeTuple
{
    public float time;
    public Keyframe kf;
    
    public KeyframeTuple(float time, Keyframe kf)
    {
        this.time = time;
        this.kf = kf;
    }
}

public class KeyframeSet
{
    public SortedList keyframes;

    public KeyframeSet()
    {
        keyframes = new SortedList();
    }

    public void AddKeyframe(float time, Keyframe kf)
    {
        if(keyframes.ContainsKey(time))
        {
            keyframes.Remove(time);
        }
        keyframes.Add(time, kf);
    }

    public void RemoveKeyframe(float time)
    {
        keyframes.Remove(time);
    }

    public float GetTime(Keyframe kf)
    {
        return (float) keyframes.GetKey(keyframes.IndexOfValue(kf));
    } 

    public Keyframe GetKeyframe(float time)
    {
        return (Keyframe) keyframes.GetByIndex(keyframes.IndexOfKey(time));
    }

    public KeyframeTuple GetKeyframeTuple(float time)
    {
        return (KeyframeTuple) new  KeyframeTuple(time, GetKeyframe(time));
    }

    // Next KeyframeTuple Methods
    public Keyframe GetNextKeyframe(float time)
    {
        // get the last keyframe if time exceeds the time of the last frame
        if(time >= (float)keyframes.GetKey(keyframes.Count-1))
        {
            return (Keyframe) keyframes.GetByIndex(keyframes.Count-1);
        }
        // get the first keyframe if time is less than the time of the first frame
        else if( time <= (float)keyframes.GetKey(0))
        {
            return (Keyframe) keyframes.GetByIndex(0);
        }

        int i = 0;
        for (i = 0; i < keyframes.Count-1; i++)
        {
            float currT = (float) keyframes.GetKey(i);
            float nextT = (float) keyframes.GetKey(i+1);
            if(time >= currT && time < nextT)
                break;
        }
        return (Keyframe) keyframes.GetByIndex(i+1);
    }

    public float GetNextTime(float time)
    {
        return GetTime(GetNextKeyframe(time));
    }

    public KeyframeTuple GetNextKeyframeTuple(float time) {
        return new KeyframeTuple(GetNextTime(time), GetNextKeyframe(time));
    }

    // Previous KeyframeTuple Methods
    public Keyframe GetPreviousKeyframe(float time)
    {
        if(time >= (float)keyframes.GetKey(keyframes.Count-1))
        {
            return (Keyframe) keyframes.GetByIndex(keyframes.Count-1);
        }
        else if( time <= (float)keyframes.GetKey(0))
        {
            return (Keyframe) keyframes.GetByIndex(0);
        }

        int i = 1;
        for (i = 1; i < keyframes.Count; i++)
        {
            float currT = (float) keyframes.GetKey(i-1);
            float nextT = (float) keyframes.GetKey(i);
            if(time >= currT && time < nextT)
                break;
        }
        return (Keyframe) keyframes.GetByIndex(i-1);     
    }

    public float GetPreviousTime(float time)
    {
        return GetTime(GetPreviousKeyframe(time));
    }

    public KeyframeTuple GetPreviousKeyframeTuple(float time) { 
        return new KeyframeTuple(GetPreviousTime(time), GetPreviousKeyframe(time));
    }

    // Interpolates between keyframes
    public Keyframe GetInterpolatedKeyframe(float time)
    {
        if (time > (float) keyframes.GetKey(keyframes.Count-1)){
            return (Keyframe) keyframes.GetByIndex(keyframes.Count-1);
        }
        if (time == 0 && GetPreviousTime(time) == 0 ) {
            return (Keyframe) keyframes.GetByIndex(0);
        }
        if (GetPreviousTime(time) == GetNextTime(time)) {
            return (Keyframe) keyframes.GetByIndex(0);
        }

        Keyframe prevkf = GetPreviousKeyframe(time);
        Keyframe nextkf = GetNextKeyframe(time);

        float timeRatio = (time - GetPreviousTime(time)) / 
                           (GetNextTime(time) - GetPreviousTime(time));
        
        
        return new Keyframe(Vector3.Lerp(prevkf.pos, nextkf.pos, timeRatio), 
                            Quaternion.Slerp(prevkf.rot, nextkf.rot, timeRatio));
    }    

    public KeyframeTuple GetInterpolatedKeyframeTuple(float time) {
        return new KeyframeTuple(time, GetInterpolatedKeyframe(time));
    }
}
