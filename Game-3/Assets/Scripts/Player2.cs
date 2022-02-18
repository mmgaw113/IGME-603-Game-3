using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player2 : MonoBehaviour
{
    public int health = 3;
    public int turnNumber = 1;
    // Start is called before the first frame update
    PlayerTurnLogic turnLogic;
    TileManager currentTile;
    public TextMeshProUGUI text;

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
        text.text = "Player 2 Health: " + health;
    }

    void TakeDamage(TileManager tile)
    {
        if (tile == currentTile && health > 0)
            health--;
    }
}


