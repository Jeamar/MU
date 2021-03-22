using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour {
	private bool canFall;
	private Vector3 direction;
	Quaternion rot = new Quaternion();
	float timeCount;
	// Use this for initialization
	void Start ()
	{
		canFall = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (canFall) {
			Quaternion rotation = Quaternion.LookRotation(transform.position - this.direction);
			rotation *= Quaternion.Euler(0, 0, (transform.position.x - this.direction.x >= 0) ? -90f : 90f);
			//rot.Set(0, 0, rotation.z, rotation.w);
			Quaternion rota = new Quaternion (0, 0, rotation.z, rotation.w);
			transform.rotation = Quaternion.Slerp(rot, rota, timeCount);
        	timeCount = timeCount + Time.deltaTime;
			//transform.rotation = Quaternion.Lerp(rot, rota, Time.time * 0.5f);
			
		}
		else
		{
			Quaternion rotation = Quaternion.LookRotation(transform.position - this.direction);
			rotation *= Quaternion.Euler(0, 0, (transform.position.x - this.direction.x >= 0) ? -90f : 90f);
			rot.Set(0, 0, rotation.z, rotation.w);
			transform.rotation = rot;
		}
	}

	public void startFalling(Vector3 up)
	{
		Quaternion rot = transform.rotation;
		this.direction = up;
		this. canFall = true;
	}

	public void stopFalling()
	{
		this.canFall = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		this.stopFalling();
		//gameObject.transform.parent = collision.transform;
	}
}
