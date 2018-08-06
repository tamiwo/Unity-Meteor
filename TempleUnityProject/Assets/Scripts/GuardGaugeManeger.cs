using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGaugeManeger : MonoBehaviour {

    public GameObject barMask;

    public float max = 20.0f;           //最大値
    public float dafault = 20.0f;       //初期値
    public float lossRate = 1.0f;       //ガード中に減少量[/s]
    public float gainRate = 1.0f;       //回復中の増加量[/s]
    public float lostByMeteor = 7.0f;   //隕石と接触したときに減らす量
    public float power = 0f;
    public GameObject guardShape;

    private Vector3 barScaleOrigin;
    private Vector3 barScale;
    private float scale = 0.0f;

	// Use this for initialization
    void Start () {
        //初期値(100%)の長さを取得する
        barScale = barMask.transform.localScale;
        barScaleOrigin = new Vector3(barScale.x, barScale.y);

        power = dafault;
        SetScale(power / max);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("GaurdShape pow:" + power + "(" + guardShape.activeSelf + ")" );

        if (guardShape.activeSelf == true)// ガード中
        {
            power -= Time.deltaTime * lossRate;
            SetScale(power / max);
            if (power < 0)
            {
                guardShape.GetComponent<GuardShapeManager>().GuardShapeInactive();
            }
        }
        else { // ガードしてない
            if (power < max) //回復中
            {
                power += Time.deltaTime * gainRate;
                SetScale(power / max);
            }
        } 
    }



    void SetScale(float scale) {

        // 0から1の範囲に収める
        if (scale > 1.0f)
        {
            scale = 1.0f;
        }
        else if (scale < 0.0f)
        {
            scale = 0.0f;
        }
        //Debug.Log("set scale" + scale );
        barMask.transform.localScale = new Vector3(barScaleOrigin.x * scale, barScaleOrigin.y);

    }
}
