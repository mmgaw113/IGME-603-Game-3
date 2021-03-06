using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int gridSize;
    [SerializeField] private float tileDistance;
    public int GridSize { get => gridSize; }
    public float TileDistance { get => tileDistance; }

    [SerializeField] GameObject tilePrefab;
    TileManager[,] tiles;

    public static Action<int, bool> attackQuadrant;

    void Start()
    {
        tiles = new TileManager[gridSize, gridSize];

        //Instantiate the tiles

        //Z position
        for (int i = 0; i < gridSize; i++)
        {
            //X position
            for (int j = 0; j < gridSize; j++)
            {
                Vector3 pos = new Vector3(tileDistance * j, 0, tileDistance * i);
                GameObject obj = Instantiate(tilePrefab, pos + transform.position, Quaternion.identity);
                obj.name = "Tile [" + j + ", " + i + "]";
                TileManager tile = obj.GetComponent<TileManager>();

                tiles[j, i] = tile;
            }
        }

        //Z position
        for (int i = 0; i < gridSize; i++)
        {
            //X position
            for (int j = 0; j < gridSize; j++)
            {
                tiles = tiles[j, i].ConnectTiles(tiles, new Vector2(j, i), gridSize);
            }
        }
    }

    private void OnEnable() { attackQuadrant += OnAttackQuadrant; }
    private void OnDisable() { attackQuadrant -= OnAttackQuadrant; }

    private void OnAttackQuadrant(int quadNum, bool isPlayer1 = false)
    {
        //Init assuming upper left corner, therefore, RightBack points to center
        TileManager tile = null;
        var toCenter = (x: GridDirection.Right, y: GridDirection.Back);

        //Assuming the quadrants are 1, 2, 3, and 4 from left to right top to bottom, assign the
        //extreme corner of that quadrant to quadCorner, and update toCenter accordingly
        switch (quadNum)
        {
            //upper left
            case 1:
                tile = tiles[0, gridSize - 1];
                break;
            //upper right
            case 2:
                tile = tiles[gridSize - 1, gridSize - 1];
                toCenter.x = GridDirection.Left;
                break;
            //bottom left
            case 3:
                tile = tiles[0, 0];
                toCenter.y = GridDirection.Forward;
                break;
            //bottom right
            case 4:
                tile = tiles[gridSize - 1, 0];
                toCenter.x = GridDirection.Left;
                toCenter.y = GridDirection.Forward;
                break;
            default:
                Debug.LogError($"GridManager: Cannot attack quadrant {quadNum}! No attack performed.");
                return;
        }

        Debug.Log($"<color=#ff0000>! Attack on quadrant {quadNum} engaged; Starting with {tile.name}, from " +
            $"{toCenter.x} to {toCenter.y}. !</color>");

        //Quadrants are half the grid size in both dimensions.
        //  In the case of odd grid sizes, the "quadrants" will overlap each other by one row/column
        int quadSize = Mathf.CeilToInt(gridSize / 2f);
        for (int i = 0; i < quadSize; i++)
        {
            //Attack self, then attack along the X. Then move down to the next row.
            tile.AttackTile(isPlayer1);
            tile.AttackTile(toCenter.x, quadSize - 1, isPlayer1);
            tile = tile.GetAdjTile(toCenter.y);
        }
    }

    public TileManager GetTile(int xIndex, int yIndex)
    {
        return tiles[xIndex, yIndex];
    }
}
