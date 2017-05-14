using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TOUCH_INFO
{
    /// タッチなし
    None = -1,
    // 以下は UnityEngine.TouchPhase の値に対応
    /// タッチ開始
    Began = 0,
    /// タッチ移動
    Moved = 1,
    /// タッチ静止
    Stationary = 2,
    /// タッチ終了
    Ended = 3,
    /// タッチキャンセル
    Canceled = 4,
}
public class DenQ_Input : MonoBehaviour
{
    private static Vector3 TouchPosition = Vector3.zero;
    private static Vector3 PrevTouchPosition = Vector3.zero;


    public static TOUCH_INFO GetTouch()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0)) { return TOUCH_INFO.Began; }
            if (Input.GetMouseButton(0)) { return TOUCH_INFO.Moved; }
            if (Input.GetMouseButtonUp(0)) { return TOUCH_INFO.Ended; }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                return (TOUCH_INFO)((int)Input.GetTouch(0).phase);
            }
        }
        return TOUCH_INFO.None;
    }
    public static Vector3 GetTouchPosition()
    {
        if (Application.isEditor)
        {
            TOUCH_INFO touch = DenQ_Input.GetTouch();
            if (touch != TOUCH_INFO.None)
            {
                PrevTouchPosition = Input.mousePosition;
                return PrevTouchPosition;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                TouchPosition.x = touch.position.x;
                TouchPosition.y = touch.position.y;
                return TouchPosition;
            }
        }
        return Vector3.zero;
    }
    public static Vector3 GetDeltaPosition()
    {
        if (Application.isEditor)
        {
            var info = (TOUCH_INFO)DenQ_Input.GetTouch();
            if (info != TOUCH_INFO.None)
            {
                Vector3 currentPosition = Input.mousePosition;
                Vector3 delta = currentPosition - TouchPosition;
                TouchPosition = currentPosition;
                return delta;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                TouchPosition.x = touch.deltaPosition.x;
                TouchPosition.y = touch.deltaPosition.y;
                return TouchPosition;
            }
        }
        return Vector3.zero;
    }
    public static Vector3 GetTouchWorldPosition(Camera camera)
    {
        return camera.ScreenToWorldPoint(GetTouchPosition());
    }
    public static Ray GetScreenRay(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(GetTouchPosition());
        return ray;
    }


}