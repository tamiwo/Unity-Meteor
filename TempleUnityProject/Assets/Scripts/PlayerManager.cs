using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	public GameObject gameManager;				//ゲームマネージャー
	public LayerMask GroundLayer;				//グラウンドレイヤー
	public GameObject player;					//プレイヤー
	public GameObject AttackShape;				//アタックシェイプ
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
		JumpingPlayer ();
	}		

	void FixedUpdate() {
		//ジャンプ処理
		if (goJump) {
			rbody.AddForce (Vector2.up * jumpPower);
			goJump = false;
		}
	}

	void JumpingPlayer(){
		//ジャンプ中のレイヤー切り替え
		if (!canJump) {
			player.layer = LayerMask.NameToLayer ("JumpingPlayer");
		} else if (canJump) {
			player.layer = LayerMask.NameToLayer ("Player");
		}
	}
		
	//プレイヤーオブジェクト削除処理
	void DestroyPlayer(){
		Destroy (this.gameObject);
	}

	//ジャンプ
	public void Jump(){
		if (canJump) {
			goJump = true;
		}
	}

	//アタック
	public void Attack(){
		GameObject AttackShapePref = (GameObject)Instantiate (AttackShape);
		AttackShapePref.transform.SetParent (player.transform, false);
        AttackShapePref.GetComponent<AttackShapeManager>().AttackShapeActive();
	}
}
