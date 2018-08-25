using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Hit;

public class TouchManager : MonoBehaviour {

    //イベント関数
    [SerializeField]
    private UnityEvent swipeUpStart;
    [SerializeField]
    private UnityEvent swipeUpEnd;
    [SerializeField]
    private UnityEvent swipeUpCancel;
    [SerializeField]
    private UnityEvent swipeDownStart;
    [SerializeField]
    private UnityEvent swipeDownEnd;
    [SerializeField]
    private UnityEvent swipeDownCancel;
    [SerializeField]
    private UnityEvent swipeRightStart;
    [SerializeField]
    private UnityEvent swipeRightEnd;
    [SerializeField]
    private UnityEvent swipeRightCancel;
    [SerializeField]
    private UnityEvent swipeLeftStart;
    [SerializeField]
    private UnityEvent swipeLeftEnd;
    [SerializeField]
    private UnityEvent swipeLeftCancel;
    [SerializeField]
    private UnityEvent tap;

    // 設定値
    public static float minimumDistance = 30.0f;   //最小スワイプ判定距離


    private Vector2 _touchStartPoint;   //タッチ開始位置
    enum Direction { NONE, UP, DOWN, RIGHT, LEFT };
    private Direction _swipeDir;

    private void OnEnable()
    {
        // フリック
        //GetComponent<FlickGesture>().Flicked += OnFlick;
        // タップ
        GetComponent<TapGesture>().Tapped += OnTap;
        // 移動
        GetComponent<ScreenTransformGesture>().Transformed += OnTransform;
        GetComponent<ScreenTransformGesture>().TransformStarted += OnTransformStart;
        GetComponent<ScreenTransformGesture>().TransformCompleted += OnTransformComplete;
    }

    private void OnDisable()
    {
        // フリック
        //GetComponent<FlickGesture>().Flicked -= OnFlick;
        // タップ
        GetComponent<TapGesture>().Tapped -= OnTap;
        // 移動
        GetComponent<ScreenTransformGesture>().Transformed -= OnTransform;
        GetComponent<ScreenTransformGesture>().TransformStarted -= OnTransformStart;
        GetComponent<ScreenTransformGesture>().TransformCompleted -= OnTransformComplete;
    }

    // Transform開始時に呼ばれる
    private void OnTransformStart(object sender, System.EventArgs e){
        Debug.Log("Transform Start.");
        //タッチ位置を記録する
        ScreenTransformGesture gesture = (ScreenTransformGesture)sender;
        _touchStartPoint = gesture.ScreenPosition;
    }

    // Transform中に呼ばれる
    private void OnTransform(object sender, System.EventArgs e)
    {
        //タッチ位置を取得する
        ScreenTransformGesture gesture = (ScreenTransformGesture)sender;
        var touchPoint = gesture.ScreenPosition;
        //開始位置からの移動量をだす
        var moveVector = touchPoint - _touchStartPoint;

        // スワイプ方向判定
        Direction dir = Vec2Dirction(moveVector);
        // 方向が変わった
        if (_swipeDir != dir)
        {
            switch(_swipeDir){
                case Direction.UP:
                    swipeUpCancel.Invoke();
                    break;
                case Direction.DOWN:
                    swipeDownCancel.Invoke();
                    break;
                case Direction.RIGHT:
                    swipeRightCancel.Invoke();
                    break;
                case Direction.LEFT:
                    swipeLeftCancel.Invoke();
                    break;
            }
            _swipeDir = dir;
            Debug.Log("Swipe " + moveVector + " (" + dir + ")");
            switch (dir)
            {
                case Direction.UP:
                    swipeUpStart.Invoke();
                    break;
                case Direction.DOWN:
                    swipeDownStart.Invoke();
                    break;
                case Direction.RIGHT:
                    swipeRightStart.Invoke();
                    break;
                case Direction.LEFT:
                    swipeLeftStart.Invoke();
                    break;
                case Direction.NONE:
                    break;
            }
        }
    }

    // Transfrom終了時に呼ばれる
    private void OnTransformComplete(object sender, System.EventArgs e)
    {
        ScreenTransformGesture gesture = (ScreenTransformGesture)sender;
        var touchPoint = gesture.ScreenPosition;
        //開始位置からの移動量をだす
        var moveVector = touchPoint - _touchStartPoint;

        // スワイプ方向判定
        Direction dir = Vec2Dirction(moveVector);
        Debug.Log("Swipe " + dir + " End.");
        switch (dir)
        {
            case Direction.UP:
                swipeUpEnd.Invoke();
                break;
            case Direction.DOWN:
                swipeDownEnd.Invoke();
                break;
            case Direction.RIGHT:
                swipeRightEnd.Invoke();
                break;
            case Direction.LEFT:
                swipeLeftEnd.Invoke();
                break;
            case Direction.NONE:
                //何もしない
                break;
        }
        _swipeDir = Direction.NONE;
    }

    //スワイプ方向判定
    private Direction Vec2Dirction ( Vector2 vector ){


        // 移動量が一定以上でなければスワイプ判定せずに抜ける
        if ((Mathf.Abs(vector.x) < minimumDistance) &&
            (Mathf.Abs(vector.y) < minimumDistance))
        {
            return Direction.NONE;
        }

        if (Mathf.Abs(vector.y) >= Mathf.Abs(vector.x)) //縦方向
        {
            if (vector.y >= 0) //上
            {
                return Direction.UP;
            }
            else //下
            {
                return Direction.DOWN;
            }
        }
        else //横方向
        {
            if (vector.x >= 0) //右
            {
                return Direction.RIGHT;
            }
            else //左
            {
                return Direction.LEFT;
            }
        }
    }

    // タップ時に呼ばれるメソッド
    private void OnTap(object sender, System.EventArgs e)
    {
        Debug.Log("tap");
        tap.Invoke();
    }

    // フリックジェスチャーが成功すると呼ばれるメソッド
    private void OnFlick(object sender, System.EventArgs e)
    {
        var gesture = sender as FlickGesture;
        var flickVector = gesture.ScreenFlickVector;

        //string str = "フリック: " + gesture.ScreenFlickVector + " (" + gesture.ScreenFlickTime + "秒)";
        //Debug.Log(str);
        if ( Mathf.Abs(flickVector.y) >= Mathf.Abs(flickVector.x) ) //縦方向
        {
            if( flickVector.y >= 0 ) //上
            {
                Debug.Log("swipe up");
                swipeUpEnd.Invoke();
            }
            else //下
            {
                Debug.Log("swipe down");
                swipeDownEnd.Invoke();
            }
        }
        else //横方向
        {
            if( flickVector.x >= 0 ) //右
            {
                Debug.Log("swipe right");
                swipeRightEnd.Invoke();
            }
            else //左
            {
                Debug.Log("swipe left");
                swipeLeftEnd.Invoke();
            }
        }
    }
}
