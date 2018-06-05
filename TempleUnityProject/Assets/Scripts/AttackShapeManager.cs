using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShapeManager : MonoBehaviour {
    
	//自動消滅
	public float lifeTime = 1.5f;
	private float time = 0f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if(time < 0){
			this.gameObject.SetActive (false);
            Debug.Log("AttackShape Inactivate.");
        }
	}

	public void AttackShapeActive(){
        Debug.Log("AttackShape Activate.");
        time = lifeTime;
        this.gameObject.SetActive(true);
	}

    //衝突処理
    void OnTriggerEnter2D(Collider2D col)
    {
        var obj = col.gameObject;
        if (obj.tag == "Meteor") {
            //隕石破壊
            obj.GetComponent<OrbManager>().Destroy();
            //AttackShape無効化
            this.gameObject.SetActive(false);
        }
    }
}
