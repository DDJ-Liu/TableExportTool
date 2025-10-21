using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//TODO:??????????
//TODO:override????
//TODO:card resource deck
//TODO:check name??????
public class DictionaryDataTable
{
    private string[][] rawData = null;
    private string[] dictionary;
    public int rowCount = 0;
    public int columnCount = 0;

    //初始化用
    public bool InitializeTable(int row, int col)
    {
        rawData = new string[row][];
        dictionary = new string[col];
        for (int i=0; i<row; i++)
        {
            rawData[i] = new string[col];
        }
        rowCount = row;
        columnCount = col;
        return true;
    }

    //初始化用
    public bool setData(int row,int col,string content) {
        if (rawData == null)
        {
            Debug.Log("null out");
            return false;
        }
        rawData[row][col] = content;
        //Debug.Log(rawData[row][col]);
        return true;
    }

    public T getData<T>(int row,int col)
    {
        //Debug.Log(typeof(T));
        if (typeof(T) == typeof(int))
        {
            if (int.TryParse(rawData[row][col], out int intResult))
                return TypeConvertor<T>(intResult);
            else
            {
                Debug.Log("wrong type call on int");
                return TypeConvertor<T>(-1);
            }
        }
        if (typeof(T) == typeof(float))
        {
            if (float.TryParse(rawData[row][col], out float floatResult))
                return TypeConvertor<T>(floatResult);
            else
            {
                Debug.Log("wrong type call on float");
                return TypeConvertor<T>(-1);
            }
        }
        if (typeof(T) == typeof(string))
        {
            return TypeConvertor<T>(rawData[row][col]);
        }
        Debug.Log("wrong data type");
        return default(T);
    }

    private T TypeConvertor<T>(object obj )
    {
        try
        {
            // Safe conversion using Convert.ChangeType
            return (T)Convert.ChangeType(obj, typeof(T));
        }
        catch (InvalidCastException)
        {
            Debug.LogError($"Error: Unable to convert object of type {obj.GetType()} to {typeof(T)}.");
            return default(T);
        }
    }
       
    //以字符串的形式获取一行的全部数据
    public List<string> GetLine(int row)
    {
        List<string> list = new List<string>();
        for(int i = 0; i < columnCount; i++)
        {
            list.Add(rawData[row][i]);
            //Debug.Log(list[i]);
        }
        return list;
    }
    //以字符串的形式获取一行的全部数据，索引方式uid
    public List<string> GetLineByUID(int uid)
    {
        int row = GetRowByUID(uid);
        if(row == -1)
        {
            return null;
        }
        return GetLine(row);
    }
    //获取一行的行数，索引方式uid
    public int GetRowByUID(int uid)
    {
        int uidCol = GetColumnByName("uid");
        if (uidCol == -1)
        {
            Debug.Log("uid not set in this table");
            return -1;
        }
        else
        {
            for(int i = 0; i < rowCount; i++)
            {
                if (int.Parse(rawData[i][uidCol]) == uid)
                {
                    return i;
                }
            }
            Debug.Log("can't find matched uid");
            return -1;
        }
    }


    public T GetVariable<T>(int row,string name)
    {
        int col = GetColumnByName(name);
        if(col == -1)
        {
            return default(T);
        }
        else
        {
            return TypeConvertor<T>(rawData[row][col]);
        }
    }
    //初始化用
    public void SetDictionary(int i, string dict)
    {
        dictionary[i] = dict;
        return;
    }

    //根据一个 表头（string）name，索引列数
    public int GetColumnByName(string name)
    {
        for (int i = 0; i < columnCount; i++)
        {
            if (dictionary[i] == name)
            {
                return i;
            }
        }
        //Debug.Log("wrong variable name, not matched");
        return -1;
    }

    public string GetDictionaryNameOnColumn(int column)
    {
        //Debug.Log(dictionary[column]);
        return dictionary[column];
    }

}
