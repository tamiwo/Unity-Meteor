﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltraBarManager : MonoBehaviour {

    public GameObject barMask;
    public GameObject barPush;
    public GameObject buttonUltra;
    public GameObject buttonUltraBack;

    private Vector3 barScaleOrigin;
    private Vector3 barScale;

	// Use this for initialization
	void Start () {
        //初期値(100%)の長さを取得する
        barScale = barMask.transform.localScale;
        barScaleOrigin = new Vector3(barScale.x,barScale.y);

        SetScale(0.99f);
        disableUltra();

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SetScale (float scale) {

        // 0から1の範囲に収める
        if( scale > 1.0f ){
            scale = 1.0f;
        }
        else if ( scale < 0.0f ){
            scale = 0.0f;
        }

        if( scale >= 1.0f ){
            enableUltra();
        }
        else{
            //100%以外はアイコンの横まで伸縮させる
            scale = scale * (2.75f / 4);
        }
        barMask.transform.localScale = new Vector3(barScaleOrigin.x * scale, barScaleOrigin.y);

    }
     
    void enableUltra(){
        barPush.SetActive(true);
        buttonUltra.SetActive(true);
    }
    void disableUltra(){
        barPush.SetActive(false);
        buttonUltra.SetActive(false);
    }
}
