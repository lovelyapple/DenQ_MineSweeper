using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resource;

public class WindowBase : MonoBehaviour
{
    public WindowIndex? previousWindow;
    public virtual void Open() { gameObject.SetActive(true); }
    public virtual void Close() { gameObject.SetActive(false); }
    public virtual void OnEnable() { transform.SetAsFirstSibling(); }
    public virtual void OnDisable() { Close(); }
}
