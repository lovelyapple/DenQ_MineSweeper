using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;

public class SystemManager : MangerBase<SystemManager>
{
    enum SYSTEM_STATE
    {
        OVERHEAD,
        TABLE_DATA_INIT,
        RESOURCE_DATA_INIT,
        READY,
        PLAY,

    }
    SYSTEM_STATE sysState = SYSTEM_STATE.OVERHEAD;
    void Awake()
    {
        SetInstance(this);
    }
    void OnEnable()
    {

    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DenQLogger.Runing();
        return;
        switch (sysState)
        {
            case SYSTEM_STATE.OVERHEAD:
                TableManager.GetInstance().ReadTable();
                sysState = SYSTEM_STATE.TABLE_DATA_INIT;
                break;
            case SYSTEM_STATE.TABLE_DATA_INIT:
                if (TableManager.GetInstance().IsFinished())
                {
                    sysState = SYSTEM_STATE.RESOURCE_DATA_INIT;
                }
                break;
            case SYSTEM_STATE.RESOURCE_DATA_INIT:
                break;
            case SYSTEM_STATE.READY:
                break;
            case SYSTEM_STATE.PLAY:
                break;
        }
    }
    public IEnumerable SystemInitialize()
    {
        yield break;
    }
    public void ReadTable()
    {
    }
}
