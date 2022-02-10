using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    Dictionary<string, TileManager> tileAdj = new Dictionary<string, TileManager>();

    // Start is called before the first frame update
    void Awake()
    {
        //Populate the dictionary
        tileAdj.Add("LeftForward", null);
        tileAdj.Add("Forward", null);
        tileAdj.Add("RightForward", null);
        tileAdj.Add("Right", null);
        tileAdj.Add("RightBack", null);
        tileAdj.Add("Back", null);
        tileAdj.Add("LeftBack", null);
        tileAdj.Add("Left", null);
    }

    //Need the tile matrix, need the position
    public TileManager[,] ConnectTiles(TileManager[,] tileMatrix, Vector2 pos, int gridSize)
    {
        Debug.Log(pos);

        //Left Forward
        if(pos.x > 0 && pos.y < gridSize -1)
            SetConnection("LeftForward", tileMatrix[(int)pos.x - 1, (int)pos.y + 1]);

        //Forward
        if (pos.y < gridSize - 1)
            SetConnection("Forward", tileMatrix[(int)pos.x, (int)pos.y + 1]);

        //Right Forward
        if (pos.x < gridSize - 1 && pos.y < gridSize - 1)
            SetConnection("RightForward", tileMatrix[(int)pos.x + 1, (int)pos.y + 1]);

        //Right
        if (pos.x < gridSize - 1)
            SetConnection("Right", tileMatrix[(int)pos.x + 1, (int)pos.y]);

        //Right Back
        if (pos.x < gridSize - 1 && pos.y > 0)
            SetConnection("RightBack", tileMatrix[(int)pos.x + 1, (int)pos.y - 1]);

        //Back
        if (pos.y > 0)
            SetConnection("Back", tileMatrix[(int)pos.x, (int)pos.y - 1]);

        //Left Back
        if (pos.x > 0 && pos.y > 0)
            SetConnection("LeftBack", tileMatrix[(int)pos.x - 1, (int)pos.y - 1]);

        //Left
        if (pos.x > 0)
            SetConnection("Left", tileMatrix[(int)pos.x - 1, (int)pos.y]);

        return tileMatrix;
    }

    public void SetConnection(string key, TileManager tile)
    {      
        tileAdj[key] = tile;
        Debug.Log(key);
        Debug.Log(tileAdj[key]);
    }

    public void AttackTile(string direction, int range)
    {
        //Exit to the recursion
        if(range > 0)
        {
            //Code for attack
            Debug.Log(tileAdj[direction].gameObject.transform.position);
            tileAdj[direction].AttackTile(direction, range - 1);
        }
    }
}
