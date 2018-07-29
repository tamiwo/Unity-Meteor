using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraAttackShapeManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UltraAttackShapeActive()
    {
        Debug.Log("UltraAttackStart.");
        this.gameObject.SetActive(true);
    }

    public void UltraAttackShapeInactive()
    {
        Debug.Log("UltraAttackEnd.");
        this.gameObject.SetActive(false);
    }

    //衝突処理
    void OnTriggerStay2D(Collider2D col)
    {
        var obj = col.gameObject;
        if (obj.tag == "Meteor")
        {
            //隕石破壊
            obj.GetComponent<OrbManager>().Damage(1);
        }
    }
}
