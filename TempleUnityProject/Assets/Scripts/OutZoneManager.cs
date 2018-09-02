using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutZoneManager : MonoBehaviour {
	public GameObject gameManager;				//ゲームマネージャー
	public GameObject player;					//プレイヤー
    public GameObject Explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//衝突処理
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Meteor") {
			gameManager.GetComponent<GameManager> ().GameOver ();
			DestroyPlayer ();
            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.SetPositionAndRotation(col.gameObject.transform.position, transform.rotation);
		}
	}

	//プレイヤーオブジェクト削除処理
	void DestroyPlayer(){
		Destroy (player.gameObject);
	}
}
