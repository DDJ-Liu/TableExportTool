using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;

public static class DataSaveManager
{

    //public enum SaveType { gameData,settingData};

    public static void Save(SaveData data, string filePath)
    {
        //data.meta.lastSaveTime = DateTime.UtcNow.ToString("O");
        string jsonData = JsonUtility.ToJson(data, true);

        //filePath
        //string filePath = GetSavePath(data.GetType());


        if (data.useAesEncryption)
        {
            string encryptedData = EncryptAES(jsonData, aesKey);
            File.WriteAllText(filePath, encryptedData);
            Debug.Log("table exported: " + filePath);
        }
        else {
            File.WriteAllText(filePath, jsonData);
            Debug.Log("table exported: " + filePath);
        }
       
    }

    #region AESº”√‹
    private static readonly string aesKey = "PPTB42DoBroKKKSL";

    private static string EncryptAES(string plainText,string key)
    {
        using (Aes aes = Aes.Create()) {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

            //Base64
            return Convert.ToBase64String(encryptedBytes);
        }

    }

    private static string DecryptAES(string cipherText,string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] inputBytes = Convert.FromBase64String(cipherText);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(inputBytes,0, inputBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }


    #endregion


}
