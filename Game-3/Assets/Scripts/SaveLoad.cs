using UnityEngine;
public class SaveLoad : MonoBehaviour
{
    public Player1 player1;
    public Player3 player2;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player1);
        SaveSystem.SavePlayer(player2);
        Debug.Log("Saved");
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        player1.turnNumber = data.p1Turn;
        player1.health = data.p1Health;
        player2.health = data.p2Health;

        Vector3 p1Spawn;
        Vector3 p2Spawn;
        Debug.Log(data.p1Position);
        p1Spawn.x = data.p1Position[0];
        p1Spawn.y = data.p1Position[1];
        p1Spawn.z = data.p1Position[2];
        player1.transform.position = p1Spawn;

        p2Spawn.x = data.p2Position[0];
        p2Spawn.y = data.p2Position[1];
        p2Spawn.z = data.p2Position[2];
        player2.transform.position = p2Spawn;
        Debug.Log("Loaded");
    }
}
