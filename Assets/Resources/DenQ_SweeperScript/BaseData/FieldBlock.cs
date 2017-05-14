﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
public class FieldBlock : MonoBehaviour
{

    public FieldPos Pos;
    public float LengthToCamera = 0.0f;
    public BLOCK_TYPE BlockType = BLOCK_TYPE.NONE;
    public FIELD_ITEM ItemType = FIELD_ITEM.NONE;

    public void InitializeFieldBlock(int x, int z, BLOCK_TYPE type) { Pos = new FieldPos(x, z); BlockType = type; }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public BLOCK_TYPE GetBLockType()
    {
        return BlockType;
    }
    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
    public float GetRangeToMainCamera()
    {
        Vector3 dicVec = Camera.main.transform.position - this.gameObject.transform.position;
        LengthToCamera = dicVec.magnitude;
        return LengthToCamera;
    }
}
