﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject textGameOver;				//ゲームオーバーテキスト
	public GameObject buttons;					//操作ボタン

    //定数定義
    private const int MAX_ORB = 10;

    //オブジェクト参照
    public GameObject orbPrefab;
    public GameObject canvasGame;
    public GameObject textScore;

    //設定値
    public Vector3 createPosition = new Vector3(0f, 800.0f, 0f);

    //メンバ変数
    private int score = 0;
    private int nextScore = 100;

	// Use this for initialization
	void Start () {
        //初期オーブ生成
        CreateOrb();
        RefreshScoreText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // オーブ生成
    public void CreateOrb () {
        GameObject orb = (GameObject)Instantiate(orbPrefab);
        orb.transform.SetParent(canvasGame.transform, false);
        orb.transform.localPosition = createPosition;
    }

    //オーブ入手
    public void GetOrb (){
        score += 1;
        RefreshScoreText();
    }

	public void GameOver(){
		textGameOver.SetActive (true);
		buttons.SetActive (false);
	}

    //スコアてきすと更新
    void RefreshScoreText (){
        textScore.GetComponent<Text>().text =
            "Score：" + score + "/" + nextScore;
    }
}
