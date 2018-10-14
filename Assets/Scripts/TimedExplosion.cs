using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedExplosion : MonoBehaviour 
{
	public ParticleSystem ps1;
	public ParticleSystem ps2;
	public ParticleSystem ps3;
	public ParticleSystem ps4;
	public ParticleSystem ps5;
	public ParticleSystem ps6;
	public ParticleSystem ps7;
	public ParticleSystem ps8;
	public ParticleSystem ps9;
	public ParticleSystem ps10;
	public ParticleSystem ps11;
	public ParticleSystem ps12;
	public ParticleSystem ps13;
	public ParticleSystem ps14;
	public ParticleSystem ps15;
	public ParticleSystem ps16;
	public ParticleSystem ps17;
	public ParticleSystem ps18;

	public float w1;
	public float w2;
	public float w3;
	public float w4;
	public float w5;
	public float w6;
	public float w7;
	public float w8;
	public float w9;
	public float w10;
	public float w11;
	public float w12;
	public float w13;
	public float w14;
	public float w15;
	public float w16;
	public float w17;
	public float w18;

	private bool p1;
	private bool p2;
	private bool p3;
	private bool p4;
	private bool p5;
	private bool p6;
	private bool p7;
	private bool p8;
	private bool p9;
	private bool p10;
	private bool p11;
	private bool p12;
	private bool p13;
	private bool p14;
	private bool p15;
	private bool p16;
	private bool p17;
	private bool p18;


	// Use this for initialization
	void Start () 
	{
		Pause();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Play(float time)
	{
		if (time > w1 && !p1) { ps1.Play(); p1 = true;}
		if (time > w2 && !p2) { ps2.Play(); p2 = true;}
		if (time > w3 && !p3) { ps3.Play(); p3 = true;}
		if (time > w4 && !p4) { ps4.Play(); p4 = true;}
		if (time > w5 && !p5) { ps5.Play(); p5 = true;}
		if (time > w6 && !p6) { ps6.Play(); p6 = true;}
		if (time > w7 && !p7) { ps7.Play(); p7 = true;}
		if (time > w8 && !p8) { ps8.Play(); p8 = true;}
		if (time > w9 && !p9) { ps9.Play(); p9 = true;}
		if (time > w10 && !p10) { ps10.Play(); p10 = true;}
		if (time > w11 && !p11) { ps11.Play(); p11 = true;}
		if (time > w12 && !p12) { ps12.Play(); p12 = true;}
		if (time > w13 && !p13) { ps13.Play(); p13 = true;}
		if (time > w14 && !p14) { ps14.Play(); p14 = true;}
		if (time > w15 && !p15) { ps15.Play(); p15 = true;}
		if (time > w16 && !p16) { ps16.Play(); p16 = true;}
		if (time > w17 && !p17) { ps17.Play(); p17 = true;}
		if (time > w18 && !p18) { ps18.Play(); p18 = true;}
	}

	public void Pause()
	{
		ps1.Pause();
		ps2.Pause();
		ps3.Pause();
		ps4.Pause();
		ps5.Pause();
		ps6.Pause();
		ps7.Pause();
		ps8.Pause();
		ps9.Pause();
		ps10.Pause();
		ps11.Pause();
		ps12.Pause();
		ps13.Pause();
		ps14.Pause();
		ps15.Pause();

		p1 = false;
		p2 = false;
		p3 = false;
		p4 = false;
		p5 = false;
		p6 = false;
		p7 = false;
		p8 = false;
		p9 = false;
		p10 = false;
		p11 = false;
		p12 = false;
		p13 = false;
		p14 = false;
		p15 = false;
		p16 = false;
		p17 = false;
		p18 = false;
	}
}
