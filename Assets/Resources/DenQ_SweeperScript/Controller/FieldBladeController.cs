using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
public class FieldBladeController : MonoBehaviour
{

    public FieldNumbersController numberCtrl = null;
    public FieldPos fieldPos = new FieldPos();
    public int bombCnt = 0;
    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {
    }
    //TODO :クソーーーFieldMgr使いたい！
    public void InitializeBlade()
    {
        fieldPos = DenQHelper.ConverWoroldPosToFieldPos(this.gameObject.transform.position);

        RequestUpdate();
    }
    public void RequestUpdate()
    {
        bombCnt = 0;
        Collider[] cols = DenQHelper.GetSroundedObejcts(this.fieldPos);
        foreach (Collider col in cols)
        {
            if (col.gameObject.tag == "FieldBlock")
            {
                FieldBlock blockData = col.gameObject.GetComponent<FieldBlock>();
                if (blockData.ExistBomb())
                {
                    bombCnt++;
                }
            }
        }
        if (numberCtrl == null)
        {
            GameObject numbersObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_NUMBERS, this.gameObject, false);
            numbersObj.transform.position = this.gameObject.transform.position;
            numberCtrl = numbersObj.GetComponent<FieldNumbersController>();
            numberCtrl.UpdateNumber(bombCnt);
        }else
        {
            numberCtrl.UpdateNumber(bombCnt);
        }
        
    }
}
