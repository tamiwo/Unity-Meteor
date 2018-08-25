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
	public float jumpPower = 200000;			//ジャンプ力
	private bool canJump = false;				//地面に設置しているか否か
	private Animator animator;					//アニメーター
	private AudioSource sound01;				//SE音１
	private AudioSource sound02;				//SE音２

    private enum State {
        Standing,
        Squatting,
        Jumping,
        Falling,
    }
    private State status = State.Standing;

    // Use this for initialization
    void Start () {
		rbody = GetComponent<Rigidbody2D> ();	
		animator = GetComponent<Animator> ();
		//AudioSourceコンポーネントを取得し、変数に格納
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
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

        CheckFall();
	}

    // 落下開始チェック
    void CheckFall(){
        //すでに落下中なら何もしない
        if (status == State.Falling) return;

        //Y方向の速度がマイナスで落下開始
        if(rbody.velocity.y < 0){
            setStatus(State.Falling); 
        }
    }


	void FixedUpdate() {
	}
    		
	//プレイヤーオブジェクト削除処理
	void DestroyPlayer(){
		Destroy (this.gameObject);
	}

    //しゃがみ
    public void Squat(){
        //ガードキャンセル
        GuardEnd();
        if (status == State.Standing){
            setStatus(State.Squatting);
        }
    }

	//ジャンプ
	public void Jump(){
        if (canJump) {
            rbody.AddForce(Vector2.up * jumpPower);
            canJump = false;
            // レイヤー切り替え
            player.layer = LayerMask.NameToLayer("JumpingPlayer");
			//SE
			// 音を鳴らす
			sound01.PlayOneShot(sound01.clip);
            setStatus(State.Jumping);
		}
	}

	//アタック
	public void Attack(){
		//GameObject AttackShapePref = (GameObject)Instantiate (AttackShape);
		AttackShape.transform.SetParent (player.transform, false);
        AttackShape.GetComponent<AttackShapeManager>().AttackShapeActive();
        /*
		AnimatorStateInfo stateInfo = player.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);
		if (canJump) {
			player.GetComponent<Animator> ().SetTrigger ("isStandAttack");
		} else  {
			player.GetComponent<Animator> ().SetTrigger ("isJumpAttack");
			Debug.Log("isJump ON");
		}
		*/
        animator.SetTrigger("isAttack");
	}

    public void UltraAttack()
    {
        Debug.Log("Ultra Attack");
        //ジャンプ
        canJump = true;
        Jump();
        //アニメーション
        player.GetComponent<Animator>().SetTrigger("isJumpAttack");
        //UltraAttackShape有効化
        UltraAttackShape.transform.SetParent(player.transform, false);
        UltraAttackShape.GetComponent<UltraAttackShapeManager>().UltraAttackShapeSetActive(true);
    }

    public void GuardStart()
    {
        //しゃがみキャンセル
        setStatus(State.Standing);
        //GuardShapePref = (GameObject)Instantiate(GuardShape);
        GuardShape.transform.SetParent(player.transform, false);
        GuardShapeManager guardShape = GuardShape.GetComponent<GuardShapeManager>();
        guardShape.GuardShapeActive();
        if ( GuardShape.activeSelf == true ) //ガード可能
        {
		    animator.SetBool ("isGuard", true);
        }
    }

    public void GuardEnd()
    {
        GuardShape.GetComponent<GuardShapeManager>().GuardShapeInactive();
		animator.SetBool ("isGuard", false);
    }

    //衝突処理
    private void OnCollisionEnter2D(Collision2D col )
    {
        var obj = col.gameObject;
        Debug.Log("player collision" + obj.tag);

        if (obj.tag == "Ground")
        {
            //着地
            setStatus(State.Standing);
            //レイヤー切り替え
            player.layer = LayerMask.NameToLayer("Player");
            //ジャンプ可
            canJump = true;
			// 音を鳴らす
			sound02.PlayOneShot(sound02.clip);
            //UrtraAttackShape無効化
            UltraAttackShape.GetComponent<UltraAttackShapeManager>().UltraAttackShapeSetActive(false);

        }
    }

    void setStatus( State state ){

        Debug.Log("state " + status + " to " + state); 
        status = state;

        //アニメーションは
        animator.SetBool("isStand", false);
        animator.SetBool("isSquat", false);
        animator.SetBool("isJump", false);
        animator.SetBool("isFall", false);

        //状態が変わった時の処理
        switch( status ){
            case State.Standing:
                animator.SetBool("isStand", true);
                break;
            case State.Squatting:
                animator.SetBool("isSquat", true);
                break;
            case State.Jumping:
                animator.SetBool("isJump", true);;
                break;
            case State.Falling:
                animator.SetBool("isFall", true);
                break;
            default:
                Debug.Log("nothing to do:" + status);
                break;
        }

    }
}
