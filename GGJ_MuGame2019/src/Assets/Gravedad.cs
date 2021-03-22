using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gravedad : MonoBehaviour {
	public GameObject attractedTo;
    public float strengthOfAttraction = 5.0f;
	public GameObject[] planets;

	public bool onFloor = false;

	public float speed;
	public float JumpSpeed;
	private bool atrae = false;
	public bool salta = false;

	private bool carga = false;

	public Slider barraFuerza;
	private bool CargaPoder;
	private float PoderSalto;
	
	public GameObject Malla;
	public GameObject RotObj;
	private Animator anim;
	private float RigidVelocidad;

	private PlayerFall pf;
	private Rigidbody rb;

	public GameObject dieEffect;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		anim = Malla.GetComponent<Animator>();
		pf = GetComponent<PlayerFall>();
		planets = GameObject.FindGameObjectsWithTag("Planet");
	}
	
	// Update is called once per frame
	void Update() {
		{
			if(CargaPoder)
			{
				PoderSalto = PoderSalto * 1.02f;
				barraFuerza.value = PoderSalto;
				if(PoderSalto >= 100)
				{
					JumpSpeed = PoderSalto * 10000f;
					PoderSalto = 1;
					barraFuerza.value = 0;
					CargaPoder = false;
					atrae = false;
					salta = true;
				}
			}
			if(onFloor)
			{
				if(Input.GetKeyDown(KeyCode.Space))
				{
					carga = true;
					CargaPoder = true;
					PoderSalto = 1;

				}
				if(Input.GetKeyUp(KeyCode.Space) && carga)
				{
					anim.SetBool("Jump", true);
					if(PoderSalto < 10)
					{
						PoderSalto = 10;
					}
					transform.parent = null;
					JumpSpeed = PoderSalto * 15000f;
					PoderSalto = 1;
					barraFuerza.value = 0;
					CargaPoder = false;
					atrae = false;
					salta = true;
				}else{
					atrae = true;
				}
				if(Input.GetKey(KeyCode.D)){
					gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);
					anim.SetBool("Walk", true);
					RotObj.transform.localEulerAngles = new Vector3(RotObj.transform.localEulerAngles.x, 180, RotObj.transform.localEulerAngles.z);
				}
				if(Input.GetKey(KeyCode.A))
				{
					gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
					anim.SetBool("Walk", true);
					RotObj.transform.localEulerAngles = new Vector3(RotObj.transform.localEulerAngles.x, 0, RotObj.transform.localEulerAngles.z);
				}
				
			}
		}
	}
	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);

		if(!onFloor)
		{
			planetSelect();
			atrae = true;
			float dist = Vector3.Distance(transform.position, attractedTo.transform.position);
			if(dist < 20)
			{
				rb.drag = 3;
			}else if(dist < 50)
			{
				pf.startFalling(calculateDirection(attractedTo.transform.position));
				rb.drag = 1;			
			}else
			{
				rb.drag = 0;
			}
		}else
		{
			RigidVelocidad = rb.velocity.magnitude;
			if(RigidVelocidad < 0.2f)
			{
				anim.SetBool("Walk", false);
			}
		}

		if(atrae)
		{
			AtraccionPlaneta();
		}
		if(salta)
		{
			Vector3 jump = transform.up * Time.deltaTime;
			pf.stopFalling();
			rb.AddForce(jump*JumpSpeed);
			transform.parent = null;
			salta = false;
		}
	}

	private Vector3 calculateDirection(Vector3 playerPosition)
	{
		return (transform.position + (playerPosition - transform.position));
	}

	void AtraccionPlaneta()
	{
		Vector3 direction = attractedTo.transform.position - transform.position;
		rb.AddForce (strengthOfAttraction * direction);
	}
	void planetSelect()
	{
		for(int i = 0; i < planets.Length; i++)
		{
			float d = Vector3.Distance(transform.position, planets[i].transform.position);
			float d2 = Vector3.Distance(transform.position, attractedTo.transform.position);
			if(d < d2)
			{
				transform.parent = null;
				attractedTo = planets[i];
			}
		}
	}
	void OnCollisionEnter(Collision Col)
	{
		GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		if(Col.gameObject.tag == "Planet")
		{
			anim.SetBool("Jump", false);
			anim.SetBool("Walk", false);
		}

		if(Col.gameObject.tag == "Familia")
		{
			gm.addFamily();
			Destroy(Col.gameObject);
		}

		if (Col.gameObject.tag == "Nave")
		{
			gm.winGame(gameObject);
		}
	}
	void OnCollisionStay(Collision Col)
	{
		if(Col.gameObject.tag == "Planet")
		{
			anim.SetBool("Jump", false);
			onFloor = true;
			transform.parent = attractedTo.transform;
		}
	}
	void OnCollisionExit(Collision Col)
	{
		if(Col.gameObject.tag == "Planet")
		{
			onFloor = false;
			salta = false;
		}
	}

	public void die()
	{
		Instantiate(this.dieEffect, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
