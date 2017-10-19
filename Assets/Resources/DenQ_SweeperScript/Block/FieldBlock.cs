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
    [SerializeField] MeshRenderer numberRender;
    [SerializeField] Material[] numberMtrls;
    ///ブロックのタイプ、nullなら破壊されている
    public uint? blockType;
    public ulong? contentItemCode;
    public bool isBroken { get; private set; }

    ///ブロック情報の設定(BlockData, FielditemAfterItBroken)
    public void SetUpInfo(FieldBlockData data, ulong? itemCode)
    {
        this.hp = data.hp;
        this.blockType = data.blockType;
        this.contentItemCode = itemCode;
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
            plateObj.SetActive(false);
            return;
        }

        plateObj.SetActive(true);

        try
        {
            numberRender.material = numberMtrls[bombCnt];
        }
        catch
        {
            plateObj.SetActive(false);
        }

    }
    ///上にItemの設置
    public void SetUpItem()
    {
        if (!contentItemCode.HasValue)
        {
            return;
        }
        ResourcesManager.GetInstance().CreateFieldObjectInstance(contentItemCode.Value,this.gameObject.transform,Vector3.one);
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
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    static uint BombCnt = 0;
    public ulong itemCode = 10002000;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            BombCnt = (BombCnt + 1) % 5;
            numberRender.material = numberMtrls[BombCnt];
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            contentItemCode = itemCode;
            BreakBlock();
        }
    }
}
