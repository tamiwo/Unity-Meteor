using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShapeManager : MonoBehaviour
{
    public GameObject guardGauge;
    public float force = 100;

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
        if( guardManager.guardEnable == true ) { 
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
    void OnTriggerEnter2D(Collider2D col)
    {
        var obj = col.gameObject;
        Debug.Log("GuardShape Collision " + obj.tag);
        if (obj.tag == "Meteor")
        {
            guardManager.GuardMeteor();
            Rigidbody2D body = obj.GetComponent<Rigidbody2D>();
            body.velocity = new Vector2(0, 0);
            body.AddForce(new Vector2(0,force));
            this.gameObject.SetActive(false);
        }
    }
}