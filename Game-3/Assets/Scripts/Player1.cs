using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public int health = 3;

    PlayerTurnLogic turnLogic;
    TileManager currentTile;
    
    private void Awake()
    {
        turnLogic = gameObject.GetComponent<PlayerTurnLogic>();    
    }

    private void OnEnable()
    {
        TileManager.tileAttacked += OnTileAttack;
    }

    private void OnDisable()
    {
        TileManager.tileAttacked -= OnTileAttack;
    }

    void OnTileAttack(TileManager tile)
    {
            TakeDamage(tile);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTile = turnLogic.currentTile;
    }

    void TakeDamage(TileManager tile)
    {
        if (tile == currentTile && health > 0)
            health--;
    }
}
