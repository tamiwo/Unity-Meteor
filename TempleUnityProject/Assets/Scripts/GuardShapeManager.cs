using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShapeManager : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        GuardShapeInactive();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void GuardShapeActive()
    {
        Debug.Log("GuardShape Activate.");
        this.gameObject.SetActive(true);
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
            //GuardShape減少
            power -= lostByMeteor;
            gauge.GetComponent<GuardGaugeManeger>().SetScale(power / max);
            this.gameObject.SetActive(false);
        }
    }
}