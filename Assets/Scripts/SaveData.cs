using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

[Serializable]
public abstract class SaveData
{
    public abstract bool useAesEncryption { get; }

    public static bool UseAes<T>() where T : SaveData
    {
        var tempInstance = System.Activator.CreateInstance<T>();
        return tempInstance.useAesEncryption;
    }
}

[Serializable]
public class TableExportedData : SaveData
{
    public override bool useAesEncryption => false;

    public Dictionary<string, string>[] dataTable;

    public TableExportedData()
    {
        dataTable = new Dictionary<string, string>[0];
    }

    public TableExportedData(Dictionary<string, string>[] dataTable)
    {
        this.dataTable = new Dictionary<string, string>[dataTable.Length];
        for(int i=0; i<dataTable.Length; i++)
        {
            this.dataTable[i] = new Dictionary<string, string>(dataTable[i]);
        }
    }

}