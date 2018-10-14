using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Plays, pauses and rewinds the animation
public class SceneController : MonoBehaviour
{
    public TimedExplosion exp;
    public Animator animator;
    private bool isPlaying;
    public float animTime;
	void Start()
    {
        animTime = animator.GetCurrentAnimatorStateInfo(0).length;
        Pause();
        exp.Pause();
    }

    void Update()
    {

    }

    public float GetCurrentTime()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            return animTime;
        }
        else {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime * 
            animTime;
        }
    }

    public void Play()
    {
        isPlaying = true;
        animator.Play("Default Take");
        animator.speed = 1;
        Debug.Log("playing animation");
        exp.Play(GetCurrentTime());
    }

    public void Pause()
    {
        isPlaying = false;
        animator.speed = 0;
        Debug.Log("pausing animation");
        exp.Pause();
    }

    public void JumpToTime(float time)
    {
        float prevSpeed = animator.speed;
        bool prevPlay = isPlaying;

        animator.speed = 1;
        isPlaying = true;
        //animator.GetCurrentAnimatorStateInfo(0).normalizedTime = time/animator.GetCurrentAnimatorStateInfo(0).length;
        animator.Play("Default Take", 0, time/animTime);

        animator.speed = prevSpeed;
        isPlaying = prevPlay;
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
