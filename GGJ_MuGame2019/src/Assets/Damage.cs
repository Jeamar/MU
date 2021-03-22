using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {


	void OnCollisionEnter(Collision Col)
	{
		if(Col.gameObject.tag == "shifter")
		{
			GetComponent<Gravedad>().die();
			GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
			gm.setGameOver(true);
		}
	}
}
