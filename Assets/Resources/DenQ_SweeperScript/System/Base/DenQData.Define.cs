using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenQData
{
    ///マスターアイテムトークン
    ///先頭一桁目で決める
    public enum MASTER_ITEM_TOKEN
    {
        FIELD_ITEM = 1,
        STACK_ITEM = 2,
    }
    ///Table内で使われているトークン
    ///先頭から下から４桁目で決める
    public enum FIELD_ITEM_TOKEN
    {
        FIELD_ITEM_BLOCK = 1,
        FIELD_ITEM_BOMB = 2,
    }
    public enum PARTICLE_TYPE
    {
        ONCE_ONLY,
        RECYCLE,
        TIMES,
    }
    public enum FIELD_BLOCK
    {
        NORMAL,
    }
    public enum FIELD_ITEM
    {
        BOMB_NORMAL,
        BOMB_DELAY,
        NONE,
    }
}
