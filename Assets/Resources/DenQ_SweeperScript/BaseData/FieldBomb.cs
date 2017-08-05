using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
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
public class AI_FieldBomb_Base : AIBase
{
    public float LengthToCamera = 0.0f;
    public float life = 0;
    private BOMB_TYPE BombType = BOMB_TYPE.BOMB_NORMAL;

    void Awake()
    {
    }
    public void InitializeFieldBomb(int x, int z, FIELD_ITEM type, int fieldCode)
    {
        selfData.fieldPos = new FieldPos(x,z);
        selfData.fieldPos.unitCode = fieldCode;
        selfData.fieldPos.posX = x; selfData.fieldPos.posZ = z; BombType = ConvItemTypeToBombType(type);
        this.gameObject.transform.position = DenQHelper.ConvertFieldPosToWorld(selfData.fieldPos, selfData.fieldPos.unitCode);
        switch (BombType)
        {
            case BOMB_TYPE.BOMB_DELAY:
                life = 2.0f;//TODO 暫定デバッグ生存時間
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
            life = -Time.deltaTime;
        }
        else
        {
            //TODO フィールド大規模ユニット分割できたら、FieldManager で実行
            Collider[] cols = DenQHelper.GetSroundedObejcts(this.selfData.fieldPos, selfData.fieldPos.unitCode);
            foreach (Collider col in cols)
            {
                if (col.gameObject.tag == "FieldBlade")
                {
                    FieldPlateController pladeCtrl = col.gameObject.GetComponent<FieldPlateController>();
                    pladeCtrl.RequestUpdate();
                }
            }
            Destroy();
        }
    }
    public void Destroy()
    {
        GameObject fxObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.EFFECT_EXPLO_01, PREFAB_NAME.EFFECT_ROOT, false);
        fxObj.transform.position = this.gameObject.transform.position;
        GameObject.Destroy(this.gameObject);
    }
    public FIELD_ITEM GetTypeConverted()
    {
        return ConvBombTypeToItemType(this.BombType);
    }
    BOMB_TYPE ConvItemTypeToBombType(FIELD_ITEM itemTYpe)
    {
        switch (itemTYpe)
        {
            case FIELD_ITEM.BOMB_DELAY:
                return BOMB_TYPE.BOMB_DELAY;
            //break;
            case FIELD_ITEM.BOMB_NORMAL:
                return BOMB_TYPE.BOMB_NORMAL;
            //break;
            default: return BOMB_TYPE.BOMB_NORMAL;
        }
    }
    FIELD_ITEM ConvBombTypeToItemType(BOMB_TYPE bombType)
    {
        switch (bombType)
        {
            case BOMB_TYPE.BOMB_DELAY:
                return FIELD_ITEM.BOMB_DELAY;
            //break;
            case BOMB_TYPE.BOMB_NORMAL:
                return FIELD_ITEM.BOMB_NORMAL;
            //break;
            default: return FIELD_ITEM.BOMB_NORMAL;
        }
    }
}
