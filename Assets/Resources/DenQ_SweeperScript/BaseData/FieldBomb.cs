using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
public class FieldBomb : MonoBehaviour
{

    public FieldPos Pos;
    public float LengthToCamera = 0.0f;
    public float life = 0;
    public BOMB_TYPE BombType = BOMB_TYPE.NORMAL;
    public void InitializeFieldBomb(int x, int z, BOMB_TYPE type)
    {
        Pos.posX = x; Pos.posZ = z; BombType = type;
        switch (BombType)
        {
            case BOMB_TYPE.NORMAL:
                break;
            case BOMB_TYPE.DELAY:
                life = 5.0f;
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
        if (life > 0)
        {
            life -= Time.deltaTime * 60.0f;
        }
        else
        {
            Destroy();
        }
    }
    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
