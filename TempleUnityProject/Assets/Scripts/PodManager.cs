using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodManager : MonoBehaviour {

    public GameObject barMask;

    private Vector3 barScaleOrigin;
    private Vector3 barScale;
    private float scale = 0.0f;

    // Use this for initialization
    void Start()
    {
        //初期値(100%)の長さを取得する
        barScale = barMask.transform.localScale;
        barScaleOrigin = new Vector3(barScale.x, barScale.y);

        SetScale(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        SetScale(scale);
        scale += 0.01f;
    }

    public void SetScale(float scale)
    {

        // 0から1の範囲に収める
        if (scale > 1.0f)
        {
            scale = 1.0f;
        }
        else if (scale < 0.0f)
        {
            scale = 0.0f;
        }

        barMask.transform.localScale = new Vector3(barScaleOrigin.x, barScaleOrigin.y * scale);

    }
}
