using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionMenu : MonoBehaviour {

	public float velocidad;
	Vector3 direccion;
	// Use this for initialization
	void Start () {
		direccion = new Vector3 (1, 0, -1);
	}
	
	// Update is called once per frame
	void Update () {
		 transform.Rotate((direccion * Time.deltaTime) * velocidad);
	}
}
