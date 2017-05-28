using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
public class FieldBlock : MonoBehaviour
{

    public FieldPos Pos;
    public float LengthToCamera = 0.0f;
    public FIELD_BLOCK BlockType = FIELD_BLOCK.NORMAL;
    public FIELD_ITEM ItemType = FIELD_ITEM.NONE;

    public void InitializeFieldBlock(int x, int z, FIELD_BLOCK blockType, FIELD_ITEM itemType)
    {
        Pos = new FieldPos(x, z);
        BlockType = blockType;
        ItemType = itemType;
        switch (ItemType)
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
        return BlockType;
    }
    public FIELD_ITEM GetBlockItemType()
    {
        return ItemType;
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
    public bool ExistBomb()
    {
        if (ItemType == FIELD_ITEM.BOMB_DELAY ||
            ItemType == FIELD_ITEM.BOMB_NORMAL)
        {
            return true;
        }
        return false;
            
    }
}
