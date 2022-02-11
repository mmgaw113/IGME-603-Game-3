using UnityEngine;
public class SaveLoad : MonoBehaviour
{
    public Player1 player1;
    public Player2 player2;
    public GameData gameData;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player1);
        SaveSystem.SavePlayer(player2);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        gameData.round = data.round;
        player1.health = data.p1Health;
        player2.health = data.p2Health;

        Vector3 p1Spawn;
        Vector3 p2Spawn;
        p1Spawn.x = data.p1Position[0];
        p1Spawn.y = data.p1Position[1];
        p1Spawn.z = data.p1Position[2];

        p2Spawn.x = data.p2Position[0];
        p2Spawn.y = data.p2Position[1];
        p2Spawn.z = data.p2Position[2];
    }
}
