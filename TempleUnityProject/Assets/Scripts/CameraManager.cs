
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    
    public GameObject follow;   // カメラが追従するオブジェクト
    public Vector3 max;         // カメラの最大移動範囲
    public Vector3 min;         // カメラの最小移動範囲

    private string _name; 
    private Vector3 _offset;

	// Use this for initialization
	void Start () {	
		_name = follow.name;
		_offset = follow.transform.position;}
	
	// Update is called once per frame
    void Update () {

        //指定のオブジェクトが存在する時のみ実行
        if ( GameObject.Find(_name) ){
            
            var move = follow.transform.position - _offset;

            // 移動範囲を超える場合は範囲内に収める
            if ( move.x > max.x ){
                move.x = max.x;
            }
            if ( move.x < min.x ){
                move.x = min.x;
            }

            if (move.y > max.y)
            {
                move.y = max.y;
            }
            if (move.y < min.y)
            {
                move.y = min.y;
            }

            transform.position = new Vector3(0,move.y, -10);

        }
	}
}
