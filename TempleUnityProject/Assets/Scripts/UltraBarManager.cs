using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraBarManager : MonoBehaviour {

    public GameObject bar;

    private Vector3 scaleOrigin;

	// Use this for initialization
	void Start () {
        scaleOrigin = bar.transform.localScale;
        SetScale(0.5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void SetScale( float scale ){
        transform.localScale = new Vector3(scaleOrigin.x * scale, scaleOrigin.y);
    }
}
