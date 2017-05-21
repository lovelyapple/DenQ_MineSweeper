using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;
using DenQ.BaseStruct;
public class FieldManager : MangerBase<FieldManager>
{
    public Field FieldData = null;

    public uint FieldSizeX = 1;
    public uint FieldSizeZ = 1;

    private static bool isCreatingMap = false;
    // Use this for initialization
    void Awake()
    {
        SetInstance(this);
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void CreateField()
    {
    }
    public void CreateDebugFieldBlock()
    {
        FieldData.InsertOneBlock(new FieldPos(0, 0), FIELD_BLOCK.NORMAL,FIELD_ITEM.NONE);
    }
    public void CreateDebugFeildAll(uint sizeX, uint sizeZ)
    {
        if (!isCreatingMap)
        {
            FieldSizeX = sizeX;
            FieldSizeZ = sizeZ;
            StartCoroutine(CreateDebugFieldAllCoroutine());
        }
        else
        {
            Debug.LogError("A nothre building task is already runing");
            return;
        }
    }
    IEnumerator CreateDebugFieldAllCoroutine()
    {
        isCreatingMap = true;
        Debug.Log("start to create field");

        for (int i = 0; i < FieldSizeZ; i++)
        {
            for (int k = 0; k < FieldSizeX; k++)
            {
                FIELD_ITEM itemType = k%3 == 0? FIELD_ITEM.BOMB_DELAY :FIELD_ITEM.NONE;
                FieldData.InsertOneBlock(new FieldPos(k, i), FIELD_BLOCK.NORMAL,itemType);
                yield return null;
            }
        }
        isCreatingMap = false;

        while (true)
        {
            if (!isCreatingMap)
            {
                Debug.Log("Finished to create field");
                break;
            }
            yield return null;
        }
    }
    public void ClearField()
    {
        FieldData.ClearField();
    }
}
