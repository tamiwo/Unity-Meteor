using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShapeManager : MonoBehaviour
{
    
    public float max = 20.0f;           //最大値
    public float dafault = 20.0f;       //初期値
    public float lossRate = 1.0f;       //ガード中に減少量[/s]
    public float gainRate = 1.0f;       //回復中の増加量[/s]
    public float lostByMeteor = 7.0f;   //隕石と接触したときに減らす量
    private float power = 0f;

    // Use this for initialization
    void Start()
    {
        power = dafault;
    }

    // Update is called once per frame
    void Update()
    {
        var guardShape = this.gameObject;

        Debug.Log("GaurdShape pow:" + power );

        if (guardShape.activeSelf == true)// ガード中
        {
            power -= Time.deltaTime * lossRate;
            if (power < 0)
            {
                GuardShapeInactive();
            }
        }
        else { // ガードしてない
            if (power < max) //回復中
            {
                power -= Time.deltaTime * gainRate;;
            }
        }            
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
            this.gameObject.SetActive(false);
        }
    }
}