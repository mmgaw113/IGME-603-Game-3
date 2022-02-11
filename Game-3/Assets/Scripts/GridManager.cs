using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] int gridSize;
    [SerializeField] float tileDistance;

    [SerializeField] GameObject tilePrefab;
    TileManager[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new TileManager[gridSize, gridSize];

        //Instantiate the tiles

        //Z position
        for(int i = 0; i < gridSize; i++)
        {
            //X position
            for(int j = 0; j < gridSize; j++)
            {
                Vector3 pos = new Vector3(tileDistance * j, 0, tileDistance * i);
                GameObject obj = Instantiate(tilePrefab, pos, Quaternion.identity);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
