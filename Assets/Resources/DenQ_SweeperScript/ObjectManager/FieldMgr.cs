using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;
using DenQ.BaseStruct;
public class FieldMgr : MangerBase<FieldMgr>
{
    const string PrefabFieldPath = "DenQ_SweeperPrefab/FieldObejctRootPrefab/Field";
    public Field FieldData = null;
    protected GameObject Pfb_Field = null;

    public uint FieldSizeX = 1;
    public uint FieldSizeZ = 1;

    private static bool isCreatingMap = false;
    // Use this for initialization
    void Awake()
    {
        SetInstance(this);
        GameObject fieldObj = ResourcesHelper.LoadResourcesInstance(PrefabFieldPath, this.gameObject);
        if (fieldObj)
        {
            FieldData = fieldObj.GetComponent<Field>();
        }
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
        FieldData.InsertOneBlock(new FieldPos(0, 0), BLOCK_TYPE.NONE);
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
                FieldData.InsertOneBlock(new FieldPos(k, i), BLOCK_TYPE.NONE);
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
