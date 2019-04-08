using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerController player)
    {
      
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        Debug.Log("Saving to: " + path);

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerSave data = new PlayerSave(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerSave LoadPlayer()
    {
        
        string path = Application.persistentDataPath + "/player.fun";
        Debug.Log("Loading From: " + path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSave data =  formatter.Deserialize(stream) as PlayerSave;

            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save File Not Found in: " + path);
            return null;
        }
      
    }
}
