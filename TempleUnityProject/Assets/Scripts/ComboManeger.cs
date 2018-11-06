using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManeger : MonoBehaviour {

    private int combo = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Reset () {
        combo = 0;
    }

    public void addCombo (){
        combo += 1;
    }

    public int getCombo () {
        return combo;
    }
}
