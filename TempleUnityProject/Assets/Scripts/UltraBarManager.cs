using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraBarManager : MonoBehaviour {

    public GameObject barMask; 

    private Vector3 barScaleOrigin;
    private Vector3 barScale;

	// Use this for initialization
	void Start () {
        barScale = barMask.transform.localScale;
        barScaleOrigin = new Vector3(barScale.x,barScale.y);
        SetScale(0.5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SetScale( float scale ){
        barMask.transform.localScale = new Vector3(barScaleOrigin.x * scale, barScaleOrigin.y);
    }
}
