using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour {
	public GameObject meteorListSpawn;
	public GameObject meteor;
	public float spawnRate;
	private float rate;
	private bool canSpawn;

	// Use this for initialization
	void Start () {
		canSpawn = true;
		rate = this.spawnRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (canSpawn) {
			canSpawn = false;
			Invoke("spawnMeteor", rate);
		}
	}

	private void spawnMeteor()
	{
		this.rate = Random.Range(spawnRate * 0.5f, spawnRate * 1.5f);
		int index = Random.Range(0, this.meteorListSpawn.transform.childCount);

		Transform spawn = this.meteorListSpawn.transform.GetChild(index).transform;

		Instantiate(meteor, spawn.position, spawn.rotation);
		canSpawn = true;
	}
}
