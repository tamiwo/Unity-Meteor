using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraBarManager : MonoBehaviour {

    private Vector3 scaleOrigin;

	// Use this for initialization
	void Start () {
        Vector3 scale = transform.localScale;
        scaleOrigin = new Vector3(scale.x,scale.y);
        SetScale(0.5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void SetScale( float scale ){
        transform.localScale = new Vector3(scaleOrigin.x * scale, scaleOrigin.y);
    }
}
