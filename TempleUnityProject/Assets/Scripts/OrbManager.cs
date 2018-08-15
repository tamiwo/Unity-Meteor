using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour {

    public GameObject breakParticle;

    //オブジェクト参照
    private GameObject gameManager;
    private int HP;
    private Vector3 baseScale;

    // 生成時にHPを指定する
    public void SetHP(int hp)
    {
        HP = hp;
    }

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        //HPに合わせてサイズ調整
        baseScale = new Vector3(transform.localScale.x, transform.localScale.y);
        setScale();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //オーブ取得
    public void TouchOrb () {
        //Debug.Log("touch the orb.");
        if (Input.GetMouseButton (0) == false ){
            return;
        }

        gameManager.GetComponent<GameManager>().GetOrb();
        gameManager.GetComponent<GameManager>().CreateOrb();
        Destroy(this.gameObject);
    }

    public void Damage ( int damage )
    {
        gameManager.GetComponent<GameManager>().GetOrb();
        HP -= damage;
        Debug.Log("Meteor HP:" + HP );
        //パーティクル生成
        breakParticle = (GameObject)Instantiate(breakParticle);
        breakParticle.transform.SetPositionAndRotation(transform.position,transform.rotation);
        if ( HP <= 0 )
        {
            Destroy();
        }
        else
        {
            setScale();
            //transform.localScale *= (1 + HP / 10);// new Vector3(HP, HP);
        }
    }

    //破壊処理
    public void Destroy () {
        gameManager.GetComponent<GameManager>().CreateOrb();
        Destroy(this.gameObject);
    }

    void setScale(){
        transform.localScale = baseScale * (1.0f + (float)HP / 10.0f);
        Debug.Log(transform.localScale.ToString());
    }

}
