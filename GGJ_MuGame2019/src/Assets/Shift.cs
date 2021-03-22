using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shift : MonoBehaviour {

	public GameObject[] shifters;
	private bool canShift;
	private bool coolDown;
	private bool pinchosCoolDown;
	public float shiftRate;
	public float pinchosRate;
	public Slider slider;
	private float timer;
	private float pinchosTimer;

	public GameObject flash;
	

	// Use this for initialization
	void Start () {
		shifters = GameObject.FindGameObjectsWithTag("shifter");
		canShift = true;
		coolDown = false;
		pinchosCoolDown = false;
		timer = this.shiftRate;
		pinchosTimer = this.pinchosRate;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftShift) && this.canShift) {
			canShift = false;
			this.slider.value = 0;
			turnshifters();
			Invoke("turnshifters", this.pinchosRate);
		}
		if (coolDown) {
			timer -= Time.deltaTime;
			slider.value = this.shiftRate - timer;
		}
		if (pinchosCoolDown) {
			pinchosTimer -= Time.deltaTime;
			slider.value = pinchosTimer;
		}
	}

	void turnshifters(){
		for(int i=0; i<shifters.Length; i++){
			if(shifters[i].activeSelf){
				StartCoroutine(flashLight());
				shifters[i].SetActive(false);
				Invoke("disablePinchosCoolDown", this.pinchosRate);
				this.pinchosCoolDown = true;
				slider.maxValue = this.pinchosRate;
			}
			else {
				StartCoroutine(flashLight());
				shifters[i].SetActive(true);
				Invoke("ableShift", shiftRate);
				this.coolDown = true;
				slider.maxValue = this.shiftRate;
			}
		}
	}

	private void ableShift()
	{
		this.coolDown = false;
		this.timer = this.shiftRate;
		this.canShift = true;
	}

	private void disablePinchosCoolDown()
	{
		this.pinchosCoolDown = false;
		this.pinchosTimer = this.pinchosRate;
	}

	IEnumerator flashLight()
	{
		flash.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		flash.SetActive(false);
	}
}
