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
    private UnityEvent swipeUp;
    [SerializeField]
    private UnityEvent swipeDown;
    [SerializeField]
    private UnityEvent swipeRight;
    [SerializeField]
    private UnityEvent swipeLeft;
    [SerializeField]
    private UnityEvent tap;

    // 設定値
    public static float minimumDistance = 30.0f;   //最小スワイプ判定距離


    private Vector2 _touchStartPoint;   //タッチ開始位置
    enum Direction { UP, DOWN, RIGHT, LEFT };

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

        // 移動量が一定以上でなければスワイプ判定せずに抜ける
        if ( ( moveVector.x < minimumDistance ) && 
             ( moveVector.y < minimumDistance ) ){
            return;
        }

        // スワイプ方向判定
        Direction dir = Vec2Dirction(moveVector);
        Debug.Log("Swipe " + moveVector + " (" + dir + ")");
        switch( dir ){
            case Direction.UP:
                break;
            case Direction.DOWN:
                break;
            case Direction.RIGHT:
                break;
            case Direction.LEFT:
                break;
        }
    }

    // Transfrom終了時に呼ばれる
    private void OnTransformComplete(object sender, System.EventArgs e)
    {
        ScreenTransformGesture gesture = (ScreenTransformGesture)sender;
        var touchPoint = gesture.ScreenPosition;
        //開始位置からの移動量をだす
        var moveVector = touchPoint - _touchStartPoint;

        // 移動量が一定以上でなければスワイプ判定せずに抜ける
        if ((moveVector.x < minimumDistance) &&
             (moveVector.y < minimumDistance))
        {
            
            return;
        }

        // スワイプ方向判定
        Direction dir = Vec2Dirction(moveVector);
        Debug.Log("Swipe " + dir + " End.");
        switch (dir)
        {
            case Direction.UP:
                swipeUp.Invoke();
                break;
            case Direction.DOWN:
                swipeDown.Invoke();
                break;
            case Direction.RIGHT:
                swipeRight.Invoke();
                break;
            case Direction.LEFT:
                swipeLeft.Invoke();
                break;
        }
    }

    //スワイプ方向判定
    private Direction Vec2Dirction ( Vector2 vector ){

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
