using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGaugeManeger : MonoBehaviour {

    public GameObject barMask;
    public GameObject pod1Mask;
    public GameObject pod2Mask;

    public float max = 90.0f;           //最大値
    public float dafault = 90.0f;       //初期値
    public float lossRate = 10.0f;       //ガード中の減少量[/s]
    public float gainRate = 5.0f;       //回復中の増加量[/s]
    public float lostByMeteor = 30.0f;   //隕石と接触したときに減らす量
    public float power = 0f;

    private Vector3 barScaleOrigin;
    private Vector3 pod1MaskScaleOrigin;
    private Vector3 pod2MaskScaleOrigin;

	// Use this for initialization
    void Start () {
        //初期値(100%)の長さを取得する
        Vector3 scale = barMask.transform.localScale;
        barScaleOrigin = new Vector3(scale.x, scale.y);
        scale = pod1Mask.transform.localScale;
        pod1MaskScaleOrigin = new Vector3(scale.x, scale.y);
        scale = pod2Mask.transform.localScale;
        pod2MaskScaleOrigin = new Vector3(scale.x, scale.y);

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
        barMask.transform.localScale = new Vector3(barScaleOrigin.x * scale, barScaleOrigin.y);
        pod1Mask.transform.localScale = new Vector3(pod1MaskScaleOrigin.x, pod1MaskScaleOrigin.y * scale);
        pod2Mask.transform.localScale = new Vector3(pod2MaskScaleOrigin.x, pod2MaskScaleOrigin.y * scale);
    }
}
