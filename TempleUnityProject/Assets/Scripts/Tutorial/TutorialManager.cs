using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public GameObject gameManagerObj;
    private GameManager gameManager;

    enum STATE{
        START,
        ATTACK,
        JUMP,
        GUARD,
        END
    }
    STATE status = STATE.START;

	// Use this for initialization
	void Start () {
        gameManager = gameManagerObj.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
        switch(status){
            case STATE.START:
                //隕石が出されていたら消す
                GameObject meteor = GameObject.FindGameObjectWithTag("Meteor");
                if( meteor != null ){
                    Destroy(meteor);
                    status = STATE.ATTACK;
                    Debug.Log("Destory Meteor");
                }
                break;
            default:
                break;
        }

	}
}
