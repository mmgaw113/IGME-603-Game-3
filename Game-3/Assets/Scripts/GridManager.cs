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

    public TileManager GetTile(int xIndex, int yIndex)
    {
        return tiles[xIndex, yIndex];
    }
}
