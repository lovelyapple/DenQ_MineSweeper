using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
using DenQ.BombDelegate;

//ここしか使わせないようにする(暫定)
//他のところに公開したら、名前征討戦が始まる
namespace DenQ.BombDelegate
{
    public enum BOMB_TYPE
    {
        BOMB_DELAY = FIELD_ITEM.BOMB_DELAY,
        BOMB_NORMAL = FIELD_ITEM.BOMB_NORMAL,
    }
}
public class FieldBomb : MonoBehaviour
{
    public FieldPos Pos = new FieldPos();
    public float LengthToCamera = 0.0f;
    public float life = 0;
    private BOMB_TYPE BombType = BOMB_TYPE.BOMB_NORMAL;

    void Awake()
    {
        Debug.Log(" one bomb is created");
    }
    public void InitializeFieldBomb(int x, int z, FIELD_ITEM type)
    {
        Pos.posX = x; Pos.posZ = z; BombType = ConvItemTypeToBombType(type);
        this.gameObject.transform.position = DenQHelper.ConvertFieldPosToWorld(Pos);
        switch (BombType)
        {
            case BOMB_TYPE.BOMB_DELAY:
                life = 300.0f;//TODO 暫定デバッグ生存時間
                break;
            case BOMB_TYPE.BOMB_NORMAL:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (life > 0)
        {
            life -= Time.deltaTime * 60.0f;
        }
        else
        {
            Debug.Log("this bomb is dead");
            Destroy();
        }
    }
    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
    public FIELD_ITEM GetTypeConverted()
    {
        return ConvBombTypeToItemType(this.BombType);
    }
    BOMB_TYPE ConvItemTypeToBombType(FIELD_ITEM itemTYpe)
    {
        switch(itemTYpe)
        {
            case FIELD_ITEM.BOMB_DELAY:
            return BOMB_TYPE.BOMB_DELAY;
            //break;
            case FIELD_ITEM.BOMB_NORMAL:
            return BOMB_TYPE.BOMB_NORMAL;
            //break;
            default:return BOMB_TYPE.BOMB_NORMAL;
        }
    } 
    FIELD_ITEM ConvBombTypeToItemType(BOMB_TYPE bombType)
    {
         switch(bombType)
        {
            case BOMB_TYPE.BOMB_DELAY:
            return FIELD_ITEM.BOMB_DELAY;
            //break;
            case BOMB_TYPE.BOMB_NORMAL:
            return FIELD_ITEM.BOMB_NORMAL;
            //break;
            default:return FIELD_ITEM.BOMB_NORMAL;
        }       
    }
}
