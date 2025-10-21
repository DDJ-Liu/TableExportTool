using UnityEngine;

public class TestCase:MonoBehaviour
{
    public void Start()
    {
        string filePath = "D:/GameProject/Factory/FactoryGame_Git/Assets/StreamingAssets";
        string targetFolder = "D:/GameProject/Factory/Test";

        TableExporter.ExportAllExcelTables(filePath, targetFolder);
    }
}
