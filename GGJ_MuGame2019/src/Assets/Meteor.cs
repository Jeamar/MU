using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

	public float speed;
	private float spd;
	
	void Start()
	{
		spd = Random.Range(speed * 0.75f, speed * 1.25f);
	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.up * Time.deltaTime * spd;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Edge") {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Planet")
		{
			Destroy(gameObject);
		}
	}
}
