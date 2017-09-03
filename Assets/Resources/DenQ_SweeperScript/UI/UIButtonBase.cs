using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonBase : MonoBehaviour
{
    [SerializeField] GameObject functionTargetObject = null;
    [SerializeField] string functionName = null;
    // Use this for initialization
    public enum EVENTTYPE
    {
        onClick, onPush, onRelease,
    }
    [SerializeField] EVENTTYPE eventType;
    public bool isButtonLocked { get; private set; }
    public bool touching { get; private set; }
    private bool touchingPrev = false;
    void Update()
    {
        if (isButtonLocked)
        {
            return;
        }
        if (UpdateEvent())
        {
            if (functionTargetObject != null && functionName != null)
            {
                functionTargetObject.SendMessage(functionName,this.gameObject);
            }
        }
        touchingPrev = touching;
    }
    bool UpdateEvent()
    {
        switch (eventType)
        {
            case EVENTTYPE.onClick:
                return touchingPrev == false && touching == true;
            case EVENTTYPE.onPush:
                return touchingPrev == true && touching == true;
            case EVENTTYPE.onRelease:
                return touchingPrev == true && touching == false;
        }
        return false;
    }
    public void TouchDown()
    {
        touching = true;
    }
    public void ReleaseUp()
    {
        touching = false;
    }
    public void Reset()
    {
        touching = false;
    }
    public void LockButton()
    {
        isButtonLocked = true;
    }
    public void UnlockBUtton()
    {
        isButtonLocked = false;
    }
}
