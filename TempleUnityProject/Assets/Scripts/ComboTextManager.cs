using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboTextManager : MonoBehaviour {

    public GameObject comboManager;

	// Use this for initialization
	void Start () {
        GameObject valueTextObj = transform.Find("Base").Find("Value").gameObject;
        if ( valueTextObj == null ){
            Debug.Log("combo text object not found");
            Destroy(this.gameObject);
        }
        TextMeshPro valueText = valueTextObj.GetComponent<TextMeshPro>();
        int combo = comboManager.GetComponent<ComboManeger>().getCombo();
        valueText.SetText(combo.ToString());
        Destroy(this.gameObject, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
