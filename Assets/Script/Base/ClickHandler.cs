using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using DenQData;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public FieldObjectData fieldObjData;
    private ClickEvent clickHandler;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        SetUpFieldData();
    }
    public void SetUpFieldData(FieldObjectData data = null)
    {
        if(data != null)
        {
            this.fieldObjData = data;
            return;
        }

        if(fieldObjData != null)
        {
            return;
        }

        fieldObjData = gameObject.GetComponent<FieldObjectData>();

        if(fieldObjData == null)
        {
            Logger.GWarn("could not find FieldObjData");
        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("object touched");

        SetUpFieldData();

        if(fieldObjData != null)
        {
            FieldManager.GetInstance().ObjectTouched(fieldObjData);
        }

        //他の登録イベントがある場合
        if (clickHandler != null)
        {
            this.clickHandler.Invoke(this.gameObject);
        }
    }
    public void AddClickHandler(UnityAction<GameObject> handler)
    {
        this.clickHandler.AddListener(handler);
    }

    [System.Serializable]
    public class ClickEvent : UnityEvent<GameObject> { }
}
