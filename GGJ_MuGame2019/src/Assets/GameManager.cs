using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	private bool gameOver;
	private bool win;
	public GameObject gameOverCanvas;
	public GameObject winCanvas;
	
	private int familyCatched = 0;
	public int totalFamily;
	public Image[] starsImages;

	public Text score;
	
	// Use this for initialization
	void Start () {
		gameOver = false;
		win = false;
		score.text = this.familyCatched.ToString() + "/" + this.totalFamily.ToString();
		totalFamily = GameObject.FindGameObjectsWithTag("Familia").Length;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameOver) {
			this.gameOverCanvas.SetActive(true);
			Invoke("restartLevel", 3f);
		}
		if (this.win) {
			this.winCanvas.SetActive(true);
			
			float percentage = this.familyCatched * 100 / this.totalFamily;
			Debug.Log(percentage);
			this.showStars(percentage);
		}
	}

	public void restartLevel()
	{
		SceneManager.LoadScene("01", LoadSceneMode.Single);
	}

	public void setGameOver(bool gameOver)
	{
		this.gameOver = gameOver;
	}

	public void winGame(GameObject player)
	{
		this.win = true;
		this.winCanvas.SetActive(true);
		Destroy(player);
	}

	public void addFamily()
	{
		familyCatched++;
		score.text = this.familyCatched.ToString() + "/" + this.totalFamily.ToString();
	}

	private void showStars(float percentage)
	{
		int stars;
		if (percentage < 20f) {
			stars = 0;
		}
		else if (percentage < 50f) {
			stars = 1;
		}
		else if (percentage < 99f) {
			stars = 2;
		}
		else {
			stars = 3;
		}

		for (int i = 0; i < stars - 1; i++) {
			this.starsImages[i].gameObject.SetActive(true);
		}
	}
}
