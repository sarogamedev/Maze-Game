using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static string path = Application.persistentDataPath + "/MazeMania.a47";

    public static void SaveGame(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData(gameManager);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static SaveData LoadGame()
    {
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return saveData;
        }
        else
        {
            Debug.LogError("Save File Not Found In" + path);
            return null;
        }
    }
}
