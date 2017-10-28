using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
using DenQData;
public class FieldBlock : FieldObjectData
{
    [SerializeField] GameObject blockObj;
    [SerializeField] GameObject plateObj;
    [SerializeField] GameObject numberObj;
    [SerializeField] GameObject itemPosObj;
    [SerializeField] MeshRenderer numberRender;
    [SerializeField] Material[] numberMtrls;
    ///ブロックのタイプ、nullなら破壊されている
    public uint? blockType;
    public ulong? contentItemCode;
    public ulong ContentItemCode;
    public bool isBroken { get; private set; }
    FieldBlockData fieldBlockData;
    /// 中身のオブジェクト
    //FieldObjectData containingObjData;

    ///ブロック情報の設定(BlockData, FielditemAfterItBroken)
    public void SetUpInfo(FieldBlockData data, ulong? itemCode = null)
    {
        this.fieldBlockData = data;
        this.hp = data.hp;
        this.blockType = data.blockType;
        this.contentItemCode = itemCode;
        if (contentItemCode.HasValue)
        {
            ContentItemCode = (ulong)contentItemCode.Value;
        }
    }
    public FieldBlockData GetBlockData()
    {
        return fieldBlockData;
    }
    ///ブロック情報の更新
    public void UpdateBlock()
    {
        ///今は多分何もしなくても良い
    }
    ///ブロックの破壊
    public void BreakBlock()
    {
        blockObj.SetActive(false);
        plateObj.SetActive(true);
        if (contentItemCode.HasValue)
        {
            SetUpItem();
        }
        else
        {
            UpdatePlateInfo();
        }
    }
    ///番号の更新
    public void UpdatePlateInfo()
    {
        var bombCnt = GetSroundedBombCount();

        if (bombCnt <= 0)
        {
            numberObj.SetActive(false);
            return;
        }
        numberObj.SetActive(true);

        try
        {
            numberRender.material = numberMtrls[bombCnt];
        }
        catch
        {
            numberObj.SetActive(false);
        }

    }
    ///上にItemの設置
    void SetUpItem()
    {
        if (!contentItemCode.HasValue || itemPosObj == null)
        {
            return;
        }

        ResourcesManager.GetInstance().CreateFieldObjectInstance(contentItemCode.Value, itemPosObj.gameObject.transform, itemPosObj.transform.position);
        //TODOデータの設定
    }
    ///周囲のブロックを探知
    public List<FieldBlock> SearchBlockSrounded()
    {
        var hits = Physics.OverlapBox(gameObject.transform.position, ClientSettings.SroundBlockVector).Where(x => x.gameObject.tag == "FieldBlock").Select(x => x.gameObject);
        return hits.Select(x => x.GetComponent<FieldBlock>()).ToList();
    }
    ///周りの爆弾の数を取得する
    public uint GetSroundedBombCount()
    {
        var blocks = SearchBlockSrounded();
        return (uint)blocks.Count(block => block.isBroken && block.contentItemCode.HasValue && DenQDataBaseHelper.IsBomb(block.contentItemCode.Value));
    }
#if UNITY_EDITOR
    static uint BombCnt = 0;
    public ulong itemCode = 10002000;
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.T))
        {
            BombCnt = (BombCnt + 1) % 5;
            numberRender.material = numberMtrls[BombCnt];
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Y))
        {
            contentItemCode = itemCode;
            BreakBlock();
        }
    }
#endif
}
