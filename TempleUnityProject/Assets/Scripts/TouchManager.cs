using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TouchScript.Gestures;

public class TouchManager : MonoBehaviour {

    //イベント関数
    [SerializeField]
    private UnityEvent swipeUp;
    [SerializeField]
    private UnityEvent swipeDown;
    [SerializeField]
    private UnityEvent swipeRight;
    [SerializeField]
    private UnityEvent swipeLeft;

    private void OnEnable()
    {
        GetComponent<FlickGesture>().Flicked += OnFlick;
    }
    private void OnDisable()
    {
        GetComponent<FlickGesture>().Flicked -= OnFlick;
    }

    // フリックジェスチャーが成功すると呼ばれるメソッド
    private void OnFlick(object sender, System.EventArgs e)
    {
        var gesture = sender as FlickGesture;
        var flickVector = gesture.ScreenFlickVector;

        string str = "フリック: " + gesture.ScreenFlickVector + " (" + gesture.ScreenFlickTime + "秒)";
        Debug.Log(str);
        if ( Mathf.Abs(flickVector.y) >= Mathf.Abs(flickVector.x) ) //縦方向
        {
            if( flickVector.y >= 0 ) //上
            {
                Debug.Log("swipe up");
                swipeUp.Invoke();
            }
            else //下
            {
                Debug.Log("swipe down");
                swipeDown.Invoke();
            }
        }
        else //横方向
        {
            if( flickVector.x >= 0 ) //右
            {
                Debug.Log("swipe right");
                swipeRight.Invoke();
            }
            else //左
            {
                Debug.Log("swipe left");
                swipeLeft.Invoke();
            }
        }
    }

}
