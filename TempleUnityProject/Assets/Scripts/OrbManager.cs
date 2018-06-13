using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour {

    //オブジェクト参照
    private GameObject gameManager;
    private int HP;

    // 生成時にHPを指定する
    public void SetHP(int hp)
    {
        HP = hp;
    }

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        //HPに合わせてサイズ調整
        transform.localScale = new Vector3(HP, HP);
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
        if ( HP <= 0 )
        {
            Destroy();
        }
        else
        {
            transform.localScale = new Vector3(HP, HP);
        }
    }

    //破壊処理
    public void Destroy () {
        gameManager.GetComponent<GameManager>().CreateOrb();
        Destroy(this.gameObject);
    }

}
