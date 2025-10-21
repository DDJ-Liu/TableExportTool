using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public class TableExporter
{
    public static void ExportAllExcelTables(string sourceFolder, string targetFolder)
    {
        string[] files = Directory.GetFiles(sourceFolder);
        foreach (string file in files)
        {
            string absoluteSourcePath = Path.Combine(sourceFolder, file);
            //Debug.Log(absoluteSourcePath);
            if (Path.GetExtension(absoluteSourcePath) == ".xlsx" || Path.GetExtension(absoluteSourcePath) == ".xls")
            {
                ExportSingleExcelTable(absoluteSourcePath, targetFolder);
            }
        }

    }

    public static void ExportSingleExcelTable(string sourceFilePath, string targetFolder)
    {
        if (Path.GetExtension(sourceFilePath) == ".xlsx" || Path.GetExtension(sourceFilePath) == ".xls")
        {
            DictionaryDataTable rawDataTable = TableReader.ReadFromStreamingAssets(sourceFilePath);
            /*
            Dictionary<string, string>[] tableDict = new Dictionary<string, string>[rawDataTable.rowCount];
            for (int i = 0; i < rawDataTable.rowCount; i++) {
                tableDict[i] = new Dictionary<string, string>();
                for(int j = 0; j < rawDataTable.columnCount; j++)
                {
                    tableDict[i].Add(rawDataTable.GetDictionaryNameOnColumn(j), rawDataTable.getData<string>(i, j));
                }
            }
            TableExportedData exportedData = new TableExportedData(tableDict);
            */


            string absoluteName = Path.GetFileNameWithoutExtension(sourceFilePath);
            //Debug.Log(absoluteName);
            string targetPath = Path.Combine(targetFolder, absoluteName + ".dat");
            //Debug.Log(targetPath);
            DataSaveManager.Save(rawDataTable, targetPath);
        }
    }


}
