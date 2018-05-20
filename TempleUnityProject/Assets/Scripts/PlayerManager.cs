using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	public GameObject gameManager;				//ゲームマネージャー
	public LayerMask GroundLayer;				//グラウンドレイヤー
	private Rigidbody2D rbody;					//プレイヤー制御用
	private float jumpPower = 400;				//ジャンプ力
	private bool goJump = false;				//ジャンプしたか否か
	private bool canJump = false;				//地面に設置しているか否か



	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();		
	}
	
	// Update is called once per frame
	void Update () {
		canJump = Physics2D.Linecast (transform.position -
		(transform.right * 0.3f), transform.position - (transform.up * 0.1f), GroundLayer) ||
		Physics2D.Linecast (transform.position - (transform.right * 0.3f), transform.position - (transform.up * 0.1f), GroundLayer);
		
	}

	void FixedUpdate() {
		//ジャンプ処理
		if (goJump) {
			rbody.AddForce (Vector2.up * jumpPower);
			goJump = false;
		}
	}

	//ジャンプボタンを押した
	public void PushJumpButton() {
		if (canJump) {
			goJump = true;
		}
	}

	//衝突処理
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Trap") {
			gameManager.GetComponent<GameManager> ().GameOver ();
			DestroyPlayer ();
		}
	}

	//プレイヤーオブジェクト削除処理
	void DestroyPlayer(){
		Destroy (this.gameObject);
	}
}
