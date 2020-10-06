using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

public static class Saving
{
    public static void Save(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Space.sav";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData playerData = new PlayerData(gameManager);
        
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData Load()
    {
        string path = Application.persistentDataPath + "/Space.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerData;

        }
        else
        {
            Debug.LogError("Save file not found " + path);
            return null;
        }
    }
}
