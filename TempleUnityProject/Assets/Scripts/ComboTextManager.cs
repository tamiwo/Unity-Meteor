using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboTextManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void SetCombo (int combo) {
        GameObject valueTextObj = transform.Find("Base").Find("Value").gameObject;
        if (valueTextObj == null)
        {
            Debug.Log("combo text object not found");
            Destroy(this.gameObject);
        }
        TextMeshPro valueText = valueTextObj.GetComponent<TextMeshPro>();
        valueText.SetText(combo.ToString());
    }
}
