using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShapeManager : MonoBehaviour {

	//オブジェクト参照
	public GameObject AttackShape;			//アタックシェイプ

	/*
	//自動消滅
	public float life_time = 1.5f;
	float time = 0f;
	*/

	// Use this for initialization
	void Start () {
		//time = 0;
	}

	// Update is called once per frame
	void Update () {
		/*time += Time.deltaTime;
		print (time);
		if(time>life_time){
			AttackShape.SetActive (false);
		}
		*/
	}

	public void AttackShapeActive(){
		AttackShape.SetActive (true);
	}

    //衝突処理
    void OnTriggerEnter2D(Collider2D col)
    {
        var obj = col.gameObject;
        if (obj.tag == "Meteor") {
            obj.GetComponent<OrbManager>().Destroy();
        }
    }
}
