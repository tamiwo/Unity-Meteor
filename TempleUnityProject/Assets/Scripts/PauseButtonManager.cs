using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonManager : MonoBehaviour {

    public GameObject pauseButton;
    public GameObject restartButton;
    public GameObject pausePanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Pause(){
        Time.timeScale = 0.0f;
        pauseButton.SetActive(false);
        restartButton.SetActive(true);
        pausePanel.SetActive(true);
    }

    public void Restart(){
        Time.timeScale = 1.0f;
        pauseButton.SetActive(true);
        restartButton.SetActive(false);
        pausePanel.SetActive(false);
    }

}
