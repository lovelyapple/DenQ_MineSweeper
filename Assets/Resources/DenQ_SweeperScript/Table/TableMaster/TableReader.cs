using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableReader :MonoBehaviour
{
    public void ReadTable()
    {
        StartCoroutine(IeReadTable());
    }
    public IEnumerator IeReadTable()
    {
        Debug.Log("start");
        yield return StartCoroutine(TableManager.PreImportAll());
        yield return StartCoroutine(TableManager.ImportTableAll());
        yield return StartCoroutine(TableManager.AfterImportTablAll());
        while(!TableManager.IsFinished())
        {
            yield return null;
        }

    }  
}
