using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShapeManager : MonoBehaviour {
    
	//自動消滅
	public float lifeTime = 1.5f;
	private float time = 0f;
    public GameObject ultraGauge;

    public GameObject ParticleAttack;
    public GameObject breakParticle;
    public GameObject player;
    public GameObject comboManagerObj;
    private ComboManeger comboManeger;

    private PlayerManager playerManager;

	// Use this for initialization
	void Start () {
        playerManager = player.GetComponent<PlayerManager>();
        comboManeger = comboManagerObj.GetComponent<ComboManeger>();
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
			//パーティクル生成
			GameObject particle = (GameObject)Instantiate(ParticleAttack);
            particle.transform.SetPositionAndRotation(transform.position,transform.rotation);
            //パーティクル生成
            GameObject particle2 = Instantiate(breakParticle);
            particle2.transform.SetPositionAndRotation(transform.position, transform.rotation);
            //隕石破壊
            obj.GetComponent<OrbManager>().Damage(1);
            //コンボ増加
            comboManeger.addCombo();
            //プレイヤーの処理
            playerManager.BreakMeteor();
            //AttackShape無効化
            this.gameObject.SetActive(false);
            //ultraGauge増加
            ultraGauge.GetComponent<UltraBarManager>().AddPower(1);
        }
    }
}
