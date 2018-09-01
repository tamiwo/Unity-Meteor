using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorColliderManager : MonoBehaviour {

    public GameObject player;
    private PlayerManager playerManager;
    private bool isCollisionMeteor;
    private GameObject meteor;


	// Use this for initialization
	void Start () {
        playerManager = player.GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {

        CheckMeteorCollision();

	}

    void CheckMeteorCollision(){
        
        if (meteor == null){
            LeaveMeteor();
        }
    }


    //衝突処理
    void OnTriggerStay2D(Collider2D col)
    {
        var obj = col.gameObject;

        if (obj.tag == "Meteor")
        {
            CollisionMeteor(obj.gameObject);
        }
    }

    void CollisionMeteor(GameObject obj){

        //すでに衝突しているので何もしない
        if( isCollisionMeteor == true ) return;

        isCollisionMeteor = true;
        Debug.Log("meteor collider collision Meteor");
        playerManager.CollisionMeteor();
        meteor = obj;

    }

    void LeaveMeteor(){

        if (isCollisionMeteor == false) return;

        Debug.Log("meteor collider leave meteor");
        isCollisionMeteor = false;
        playerManager.LeaveMeteor();
    }

    //衝突処理
    void OnTriggerExit2D(Collider2D col)
    {
        var obj = col.gameObject;

        if (obj.tag == "Meteor")
        {
            LeaveMeteor();
        }
    }
}
