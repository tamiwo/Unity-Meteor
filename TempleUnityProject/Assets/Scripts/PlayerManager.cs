using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerManager : MonoBehaviour {
	public GameObject gameManager;				//ゲームマネージャー
	public LayerMask GroundLayer;				//グラウンドレイヤー
	public GameObject player;					//プレイヤー
	public GameObject AttackShape;				//アタックシェイプ
    public GameObject UltraAttackShape;         //必殺技シェイプ
    public GameObject GuardShape;               //ガードシェイプ
    private Rigidbody2D rbody;					//プレイヤー制御用
	public float jumpPower = 200000;				//ジャンプ力
	private bool goJump = false;				//ジャンプしたか否か
	private bool canJump = false;				//地面に設置しているか否か
	private Animator animator;					//アニメーター



    // Use this for initialization
    void Start () {
		rbody = GetComponent<Rigidbody2D> ();	
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Up key down");
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.A)){
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            GuardStart();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) ){
            GuardEnd();
        }
		canJump = Physics2D.Linecast (transform.position -
		(transform.right * 0.3f), transform.position - (transform.up * 0.1f), GroundLayer) ||
		Physics2D.Linecast (transform.position - (transform.right * 0.3f), transform.position - (transform.up * 0.1f), GroundLayer);
		JumpingPlayer ();
		if (!canJump) {
			animator.SetBool ("isJump", false);
		}

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
		animator.SetBool ("isJump", true);
	}

	//アタック
	public void Attack(){
		//GameObject AttackShapePref = (GameObject)Instantiate (AttackShape);
		AttackShape.transform.SetParent (player.transform, false);
        AttackShape.GetComponent<AttackShapeManager>().AttackShapeActive();
		AnimatorStateInfo stateInfo = player.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);
		if (canJump) {
			player.GetComponent<Animator> ().SetTrigger ("isStandAttack");
		} else  {
			player.GetComponent<Animator> ().SetTrigger ("isJumpAttack");
			Debug.Log("isJump ON");
		}
	}

    public void UltraAttack()
    {
        Debug.Log("Ultra Attack");
        //ジャンプ
        goJump = true;
        //アニメーション
        player.GetComponent<Animator>().SetTrigger("isJumpAttack");
        //UltraAttackShape有効化
        UltraAttackShape.transform.SetParent(player.transform, false);
        UltraAttackShape.GetComponent<UltraAttackShapeManager>().UltraAttackShapeSetActive(true);
    }

    public void GuardStart()
    {
        //GuardShapePref = (GameObject)Instantiate(GuardShape);
        GuardShape.transform.SetParent(player.transform, false);
        GuardShapeManager guardShape = GuardShape.GetComponent<GuardShapeManager>();
        guardShape.GuardShapeActive();
        if ( GuardShape.activeSelf == true ) //ガード可能
        {
		    if (canJump) {
			    animator.SetBool ("isGuard", true);
		    } else if (!canJump) {
			    animator.SetBool ("isJumpGuard", true);
		    }
        }
    }

    public void GuardEnd()
    {
        GuardShape.GetComponent<GuardShapeManager>().GuardShapeInactive();
		animator.SetBool ("isGuard", false);
		animator.SetBool ("isJumpGuard", false);
    }

    //衝突処理
    private void OnCollisionEnter2D(Collision2D col )
    {
        var obj = col.gameObject;
        Debug.Log("player collision" + obj.tag);

        if (obj.tag == "Ground")
        {
            //UrtraAttackShape無効化
            UltraAttackShape.GetComponent<UltraAttackShapeManager>().UltraAttackShapeSetActive(false);
        }
    }
}
