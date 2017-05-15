using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
public class FieldBomb : MonoBehaviour
{

    public FieldPos Pos = new FieldPos();
    public float LengthToCamera = 0.0f;
    public float life = 0;
    public BOMB_TYPE BombType = BOMB_TYPE.NORMAL;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Debug.Log(" one bomb is created");
    }
    public void InitializeFieldBomb(int x, int z, BOMB_TYPE type)
    {
        Pos.posX = x; Pos.posZ = z; BombType = type;
        switch (BombType)
        {
            case BOMB_TYPE.NORMAL:
                break;
            case BOMB_TYPE.DELAY:
                life = 300.0f;
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
            Debug.Log("this bomb is dead");
            Destroy();
        }
    }
    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
