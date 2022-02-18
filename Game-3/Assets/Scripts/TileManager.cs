using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    Dictionary<GridDirection, TileManager> tileAdj = new Dictionary<GridDirection, TileManager>();

    //Lists of the Dict so the keys and values can be seen in the inspector
#if UNITY_EDITOR
    [SerializeField] List<GridDirection> keyList = new List<GridDirection>();
    [SerializeField] List<TileManager> valueList = new List<TileManager>();
#endif

    public static Action<TileManager> tileAttacked;

    public TileIndicator indicator;
    void Awake()
    {
        //Populate the dictionary
        tileAdj.Add(GridDirection.LeftForward, null);
        tileAdj.Add(GridDirection.Forward, null);
        tileAdj.Add(GridDirection.RightForward, null);
        tileAdj.Add(GridDirection.Right, null);
        tileAdj.Add(GridDirection.RightBack, null);
        tileAdj.Add(GridDirection.Back, null);
        tileAdj.Add(GridDirection.LeftBack, null);
        tileAdj.Add(GridDirection.Left, null);

        indicator = GetComponentInChildren<TileIndicator>();
    }

    //Populates the dictionary
    public TileManager[,] ConnectTiles(TileManager[,] tileMatrix, Vector2 pos, int gridSize)
    {
        //Left Forward
        if (pos.x > 0 && pos.y < gridSize - 1)
            SetConnection(GridDirection.LeftForward, tileMatrix[(int)pos.x - 1, (int)pos.y + 1]);

        //Forward
        if (pos.y < gridSize - 1)
            SetConnection(GridDirection.Forward, tileMatrix[(int)pos.x, (int)pos.y + 1]);

        //Right Forward
        if (pos.x < gridSize - 1 && pos.y < gridSize - 1)
            SetConnection(GridDirection.RightForward, tileMatrix[(int)pos.x + 1, (int)pos.y + 1]);

        //Right
        if (pos.x < gridSize - 1)
            SetConnection(GridDirection.Right, tileMatrix[(int)pos.x + 1, (int)pos.y]);

        //Right Back
        if (pos.x < gridSize - 1 && pos.y > 0)
            SetConnection(GridDirection.RightBack, tileMatrix[(int)pos.x + 1, (int)pos.y - 1]);

        //Back
        if (pos.y > 0)
            SetConnection(GridDirection.Back, tileMatrix[(int)pos.x, (int)pos.y - 1]);

        //Left Back
        if (pos.x > 0 && pos.y > 0)
            SetConnection(GridDirection.LeftBack, tileMatrix[(int)pos.x - 1, (int)pos.y - 1]);

        //Left
        if (pos.x > 0)
            SetConnection(GridDirection.Left, tileMatrix[(int)pos.x - 1, (int)pos.y]);

#if UNITY_EDITOR
        //Adds the dictionary keys and values to the appropriate lists
        foreach (GridDirection key in tileAdj.Keys)
        {
            keyList.Add(key);
        }

        foreach (TileManager tile in tileAdj.Values)
        {
            valueList.Add(tile);
        }
#endif

        return tileMatrix;
    }

    //Helper Fnction
    public void SetConnection(GridDirection key, TileManager tile) => tileAdj[key] = tile;

    public void AttackTile(GridDirection direction, int range, bool isPlayer1 = false)
    {
        //Exit to the recursion or get out if there isn't a tile in that direction
        if (range > 0 && tileAdj[direction] != null)
        {
            tileAttacked?.Invoke(tileAdj[direction]);
            //Debug.Log($"\t<color=#ff5555>Attacked {tileAdj[direction].name}!</color>");

            //GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //obj.transform.position = tileAdj[direction].gameObject.transform.position;
            tileAdj[direction].AttackTile(direction, range - 1);

            // Play particle system
            StartCoroutine(tileAdj[direction].indicator.PlayAttackEffect(isPlayer1));
        }
    }

    public void AttackTile(bool isPlayer1 = false)
    {
        tileAttacked?.Invoke(this);

        // Play particle system
        StartCoroutine(indicator.PlayAttackEffect(isPlayer1));
        //Debug.Log($"\t<color=#ff5555>Attacked {name}!</color>");
    }

    public void AttackOrthogonal(int range)
    {
        AttackTile(GridDirection.Left, range);
        AttackTile(GridDirection.Back, range);
        AttackTile(GridDirection.Right, range);
        AttackTile(GridDirection.Forward, range);
    }
    public void AttackDiagonal(int range)
    {
        AttackTile(GridDirection.LeftBack, range);
        AttackTile(GridDirection.LeftForward, range);
        AttackTile(GridDirection.RightBack, range);
        AttackTile(GridDirection.RightForward, range);
    }

    public TileManager GetAdjTile(GridDirection direction)
    {
        return tileAdj[direction];
    }
}
