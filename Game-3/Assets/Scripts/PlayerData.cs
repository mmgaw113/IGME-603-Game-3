using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int p1Turn;
    public int p2Turn;
    public int p1Health;
    public int p2Health;
    public float[] p1Position;
    public float[] p2Position;
    public PlayerData (Player1 player1, Player2 player2)
    {
        p1Health = player1.health;
        p1Turn = player1.turnNumber;
        p1Position = new float[3];
        p1Position[0] = player1.transform.position.x;
        p1Position[1] = player1.transform.position.y;
        p1Position[2] = player1.transform.position.z;

        p2Health = player2.health;
        p2Turn = player2.turnNumber;
        p2Position = new float[3];
        p2Position[0] = player2.transform.position.x;
        p2Position[1] = player2.transform.position.y;
        p2Position[2] = player2.transform.position.z;
    }
}
