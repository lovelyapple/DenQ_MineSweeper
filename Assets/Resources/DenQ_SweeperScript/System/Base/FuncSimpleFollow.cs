using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncSimpleFollow : MonoBehaviour
{


    Vector3 diff;
    public bool followSwitch = false;
    public bool lookAtTarget = false;
    public GameObject targetObject = null;
    public float followSpeed = 0.0f;
    public float distanceMagnification = 1.0f;
    public float zoomSpeed = 0.03f;
    // Use this for initialization
    void Start()
    {
        if (targetObject != null)
        {
            diff = targetObject.transform.position - this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (followSwitch)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetObject.transform.position - diff, followSpeed);
            if(lookAtTarget)
            {
                this.transform.LookAt(targetObject.transform);
            }
        }
    }
    public void ResetTarget(GameObject newTarget)
    {
        targetObject = newTarget;
        diff = targetObject.transform.position - this.transform.position;
    }
    //カメラ用の挙動一応ここに書いとく
    public void ZoomToTarget(bool InOut)
    {
        if (InOut)
        {
            diff *= (1.0f - zoomSpeed);
        }
        else
        {
            diff *= (1.0f + zoomSpeed);
        }
    }

}
