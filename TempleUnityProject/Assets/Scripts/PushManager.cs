using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushManager : MonoBehaviour {

    Color color;
    public float flashSpeed = 3;

	void Start () {
        GetComponent<SpriteRenderer>().color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
        
        float alpha = (Mathf.Sin(Time.time* flashSpeed) + 1) / 2;
        Color newColor = new Color(1, 1, 1, alpha);
        GetComponent<SpriteRenderer>().color = newColor;
	}
}
