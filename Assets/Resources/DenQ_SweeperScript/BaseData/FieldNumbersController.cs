using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldNumbersController : MonoBehaviour
{
    [SerializeField] public GameObject posObj;
    [SerializeField] public GameObject numberObj = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateNumber(int number)
    {
        if (number <= 0 || number >= 10)
        {
            if (numberObj != null)
            {
                GameObject.Destroy(numberObj);
            }
            posObj.SetActive(true);
            return;
        }
        int nameTemp = (int)PREFAB_NAME.FIELD_NUMBER1 + (number - 1);
        numberObj = ResourcesManager.GetInstance().CreateInstance((PREFAB_NAME)nameTemp, this.gameObject, false);
        if (numberObj == null)
        {
            return;
        }

        if (posObj != null)
        {
            numberObj.transform.position = posObj.transform.position;
        }
        else
        {
            Vector3 posTemp = this.transform.position;
            posTemp.y -= 0.3f;
            numberObj.transform.position = posTemp;
        }
        posObj.SetActive(false);

    }
}
