using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player1 player1, Player1 player2)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.wow";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data1 = new PlayerData(player1, player2);

        formatter.Serialize(stream, data1);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/game.wow";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No File found)");
            return null;
        }
    }
}
