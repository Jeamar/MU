using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour {

	public float velocidad;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		 transform.Rotate((Vector3.forward * Time.deltaTime) * velocidad);
	}
}
