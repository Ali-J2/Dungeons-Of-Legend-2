using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace DungeonBrickStudios
{
    public class SerializationManager : MonoBehaviour
    {
        public static bool Save(string saveName, object saveData)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves");

            string path = Application.persistentDataPath + "/Saves/" + saveName + ".dat";

            FileStream file = File.Create(path);
            formatter.Serialize(file, saveData);
            file.Close();

            return true;
        }

        public static object Load(string path)
        {
            if (!File.Exists(path))
                return null;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            try
            {
                object save = formatter.Deserialize(file);
                file.Close();
                return save;
            }
            catch
            {
                Debug.LogErrorFormat("Failed to load at {0}", path);
                file.Close();
                return null;
            }
        }

        public static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter;
        }
    }
}
