using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
public class ParticleController : MonoBehaviour {
	public ParticleSystem particle;
	public int times;
	public PARTICLE_TYPE type = PARTICLE_TYPE.ONCE_ONLY;
	public ParticleController(){}
	public ParticleController(PARTICLE_TYPE type,int times)
	{
		this.type = type;
		this.times = times;
	}
	// Use this for initialization
	void Start () {
		if(particle == null)
		{
			DenQLogger.SError("no particleSystem!");
			GameObject.Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(particle.isPlaying);
		switch(type)
		{
			case PARTICLE_TYPE.ONCE_ONLY:
			if(!particle.isPlaying)
			{
				Remove();
			}
			break;
			case PARTICLE_TYPE.RECYCLE:
			break;
			case PARTICLE_TYPE.TIMES:
			if(!particle.isPlaying)
			{
				times--;
				if(times <= 0)
				{
					Remove();
				}else{
					particle.Play();
				}
			}
			break;
		}
	}
	public void Remove()
	{
		GameObject.Destroy(this.gameObject);
	}
}
