using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;


	void Awake ()
	{
		//SetVR ();
	}
	// Update is called once per frame
	void Update () 
	{
	//	if(target == null)
	//	{
	//	target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	//	}
		if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		
	}
/* 
	void SetVR ()
	{
		int i = PlayerPrefs.GetInt ("VR");
		GameObject VRobj = GameObject.Find ("GvrViewerMain");
		if (i == 0) {
			VRobj.GetComponent<GvrViewer> ().VRModeEnabled = false;
			Destroy (VRobj);
		} else {
			GameObject.Find ("GvrViewerMain").GetComponent<GvrViewer> ().VRModeEnabled = true;
			transform.position = transform.position + new Vector3 (0,0,-1);
		}

	}
	*/
}