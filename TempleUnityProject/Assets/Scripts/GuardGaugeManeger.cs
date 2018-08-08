using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGaugeManeger : MonoBehaviour {

    public GameObject gaugeMask;
    public GameObject podMask1;
    //public GameObject podMask2;

    public float max = 90.0f;           //最大値
    public float dafault = 90.0f;       //初期値
    public float lossRate = 10.0f;       //ガード中の減少量[/s]
    public float gainRate = 5.0f;       //回復中の増加量[/s]
    public float lostByMeteor = 30.0f;   //隕石と接触したときに減らす量
    public float power = 0f;

    private Vector3 gaugeMaskScaleOrigin;
    private Vector3 podMask1ScaleOrigin;
    //private Vector3 podMask2ScaleOrigin;

    // Use this for initialization
    void Start () {
        //初期値(100%)の長さを取得する
        Vector3 scale = gaugeMask.transform.localScale;
        gaugeMaskScaleOrigin = new Vector3(scale.x, scale.y);
        Vector2 podscale = podMask1.transform.localScale;
        podMask1ScaleOrigin = new Vector3(podscale.x, podscale.y);
        //scale = podMask2.transform.localScale;
        //podMask2ScaleOrigin = new Vector3(scale.x, scale.y);

        power = dafault;
        SetScale(power / max);
	}
	
	// Update is called once per frame
	void Update () {
        
        if (power < max) { //ガード回復
            power += Time.deltaTime * gainRate;
            SetScale(power / max);
        }
    }

    public void Guarding() {
        power -= Time.deltaTime * lossRate;
        SetScale(power / max);
    }

    public void GuardMeteor() {
        //GuardShape減少
        power -= lostByMeteor;
        SetScale(power / max);
        if (power < 0) {
            power = 0;
            //アニメーション
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
        gaugeMask.transform.localScale = new Vector3(gaugeMaskScaleOrigin.x * scale, gaugeMaskScaleOrigin.y);
        podMask1.transform.localScale = new Vector3(podMask1ScaleOrigin.x, gaugeMaskScaleOrigin.y * scale);
        Debug.Log(podMask1.transform.localScale.ToString());
        //podMask2.transform.localScale = new Vector3(podMask2ScaleOrigin.x, gaugeMaskScaleOrigin.y * scale);
    }
}
