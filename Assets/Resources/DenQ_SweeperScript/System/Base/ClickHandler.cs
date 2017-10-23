using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using DenQData;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    FieldObjectData fieldObjData;
    private ClickEvent clickHandler;
    void Awake()
    {
        if (this.fieldObjData == null)
        {
            gameObject.GetComponent<FieldObjectData>();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("object touched");
        
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
