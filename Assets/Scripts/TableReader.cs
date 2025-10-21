//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;

public class TableReader
{
    void TestData()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Environment.Version.ToString());
        //TestData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static DictionaryDataTable ReadFromStreamingAssets(string fileName,bool readFirstPage = true)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        DictionaryDataTable dataTable = new DictionaryDataTable();

        if (File.Exists(filePath))
        {
            IExcelDataReader reader = null;
            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            if (Path.GetExtension(filePath) == ".xlsx")
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else if (Path.GetExtension(filePath) == ".xls")
            {
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            }

            if (reader != null)
            {
                var result = reader.AsDataSet();
                DataTable table = result.Tables[0];
                dataTable.InitializeTable(table.Rows.Count - 1, table.Columns.Count);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    dataTable.SetDictionary(i, table.Rows[0].ItemArray[i].ToString());
                }
                for (int x = 1; x < table.Rows.Count; x++)
                {
                    //string testString = string.Empty;
                    for (int y = 0; y < table.Columns.Count; y++)
                    {
                        //Debug.Log(table.Rows[x].ItemArray[y]);
                        //testString += table.Rows[x].ItemArray[y];
                        //testString += " ";
                        dataTable.setData(x - 1, y, table.Rows[x].ItemArray[y].ToString());
                    }
                    //Debug.Log(testString);
                }
                reader.Close();
            }
        }
        else
        {
            Debug.Log("Read Error");
        }
        return dataTable;
    }
}