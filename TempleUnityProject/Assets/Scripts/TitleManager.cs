﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//スタートボタンを押した
	public void PushStartButton () {
		SceneManager.LoadScene ("GameScene");	//ステージ選択シーンへ
		Debug.Log("push start button");
	}
}
