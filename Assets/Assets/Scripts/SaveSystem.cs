using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts
{
    public static class SaveSystem 
    {
        public static readonly string path = Application.persistentDataPath + "/MazeMania.a47";

        public static void SaveGame(GameManager gameManager)
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Create);

            var saveData = new SaveData(gameManager);

            formatter.Serialize(stream, saveData);
            stream.Close();
        }

        public static SaveData LoadGame()
        {
            if(File.Exists(path))
            {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(path, FileMode.Open);

                var saveData = formatter.Deserialize(stream) as SaveData;
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
}
