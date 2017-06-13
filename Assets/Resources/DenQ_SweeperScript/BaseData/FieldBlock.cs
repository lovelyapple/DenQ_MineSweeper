﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
public class FieldBlock : MonoBehaviour
{

    public int fieldUnitCode = 0;
    public FieldPos fieldPos;
    public float lengthToCamera = 0.0f;
    public FIELD_BLOCK blockType = FIELD_BLOCK.NORMAL;
    public FIELD_ITEM itemType = FIELD_ITEM.NONE;

    public GameObject blockObj = null;
    public FieldPlateController plateCtrl = null;
    public void InitializeFieldBlock(int x, int z, FIELD_BLOCK blockType, FIELD_ITEM itemType,int fieldCode)
    {
        this.fieldUnitCode = fieldCode;
        fieldPos = new FieldPos(x, z);
        this.blockType = blockType;
        this.itemType = itemType;
        switch (this.itemType)
        {
            case FIELD_ITEM.NONE:
                break;
            case FIELD_ITEM.BOMB_DELAY:
                break;
            case FIELD_ITEM.BOMB_NORMAL:
                break;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public FIELD_BLOCK GetBLockType()
    {
        return blockType;
    }
    public FIELD_ITEM GetBlockItemType()
    {
        return itemType;
    }
    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
    public float GetRangeToMainCamera()
    {
        Vector3 dicVec = Camera.main.transform.position - this.gameObject.transform.position;
        lengthToCamera = dicVec.magnitude;
        return lengthToCamera;
    }
    public void BreakBlock()
    {
        GameObject.Destroy(blockObj);
        blockObj = null;
        GameObject plateObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_PLATE, this.gameObject, false);
        plateObj.transform.position = this.gameObject.transform.position;
        plateCtrl = plateObj.GetComponent<FieldPlateController>();
        plateCtrl.InitializePlate(fieldUnitCode);
    }
    public bool IsBroken()
    {
        return blockObj == null;
    }
    public bool ExistBomb()
    {
        if (itemType == FIELD_ITEM.BOMB_DELAY ||
            itemType == FIELD_ITEM.BOMB_NORMAL)
        {
            return true;
        }
        return false;

    }
}
