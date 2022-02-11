using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    Dictionary<string, TileManager> tileAdj = new Dictionary<string, TileManager>();

    //Lists of the Dict so the keys and values can be seen in the inspector
#if UNITY_EDITOR
    [SerializeField] List<string> keyList = new List<string>();
    [SerializeField] List<TileManager> valueList = new List<TileManager>();
#endif

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

    //Populates the dictionary
    public TileManager[,] ConnectTiles(TileManager[,] tileMatrix, Vector2 pos, int gridSize)
    {
        //Left Forward
        if (pos.x > 0 && pos.y < gridSize - 1)
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

        //Adds the dictionary keys and values to the appropriate lists
        foreach (string key in tileAdj.Keys)
        {
            keyList.Add(key);
        }

        foreach (TileManager tile in tileAdj.Values)
        {
            valueList.Add(tile);
        }

        return tileMatrix;
    }

    //Helper Fnction
    public void SetConnection(string key, TileManager tile) => tileAdj[key] = tile;

    public void AttackTile(string direction, int range)
    {
        //Exit to the recursion or get out if there isn't a tile in that direction
        if (range > 0 && tileAdj[direction] != null)
        {
            //CODE FOR ATTACK HERE

            //GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //obj.transform.position = tileAdj[direction].gameObject.transform.position;
            tileAdj[direction].AttackTile(direction, range - 1);
        }
    }
}
