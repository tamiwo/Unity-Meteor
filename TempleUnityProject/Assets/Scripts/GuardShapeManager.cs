using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShapeManager : MonoBehaviour
{
    public GameObject guardGauge;

    private GuardGaugeManeger guardManager;

    // Use this for initialization
    void Start()
    {
        guardManager = guardGauge.GetComponent<GuardGaugeManeger>();
        GuardShapeInactive();
    }

    // Update is called once per frame
    void Update()
    {
        //アクティブな間に呼ばれるので常にガード処理
        guardManager.Guarding();
        if( guardManager.power <= 0) {
            GuardShapeInactive();
        }         
    }

    public void GuardShapeActive()
    {
        if( guardManager.power > 0) { 
            Debug.Log("GuardShape Activate.");
            this.gameObject.SetActive(true);
        }
        else {
            Debug.Log("GuardShape Activate failed.");
        }
    }

    public void GuardShapeInactive()
    {
        Debug.Log("GuardShape Inactivate.");
        this.gameObject.SetActive(false);
    }

    //衝突処理
    void OnCollisionEnter2D(Collision2D col)
    {
        var obj = col.gameObject;
        Debug.Log("GuardShape Collision " + obj.tag);
        if (obj.tag == "Meteor")
        {
            guardManager.GuardMeteor();
            this.gameObject.SetActive(false);
        }
    }
}