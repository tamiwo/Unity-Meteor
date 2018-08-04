using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltraBarManager : MonoBehaviour {

    public GameObject barMask;
    public GameObject barPush;
    public GameObject buttonUltra;
    public GameObject buttonUltraBack;

    public int startPower;
    public int maxPower;
    private int power = 0;

    private Vector3 barScaleOrigin;
    private Vector3 barScale;

    private float scale = 0.0f;

	// Use this for initialization
	void Start () {
        //初期値(100%)の長さを取得する
        barScale = barMask.transform.localScale;
        barScaleOrigin = new Vector3(barScale.x,barScale.y);

        SetPower(startPower);

        disableUltra();
	}
	
	// Update is called once per frame
	void Update () {
        //SetScale(scale);
        //scale += 0.01f;
	}

    public void AddPower(int pow) {
        SetPower(power + pow);
    }

    public void SetPower(int pow) {
        power = pow;
        SetScale((float)power / (float)maxPower);
    }

    void SetScale (float scale) {

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
     
    public void enableUltra()
    {
        Debug.Log("ultra enable");
        barPush.SetActive(true);
        buttonUltra.SetActive(true);
    }
    public void disableUltra(){
        Debug.Log("ultra Disable");
        SetPower(0);
        barPush.SetActive(false);
        buttonUltra.SetActive(false);
    }
}
