using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player1 : MonoBehaviour
{
    public int health = 3;
    public int turnNumber = 1;

    PlayerTurnLogic turnLogic;

    TileManager currentTile;
    public TextMeshProUGUI text;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverScreen;

    private void Awake()
    {
        turnLogic = gameObject.GetComponent<PlayerTurnLogic>();
    }

    private void OnEnable()
    {
        TileManager.tileAttacked += CheckIfTakeDamage;
    }

    private void OnDisable()
    {
        TileManager.tileAttacked -= CheckIfTakeDamage;
    }

    private void CheckIfTakeDamage(TileManager tile)
    {
        TakeDamage(tile);
    }

    // Update is called once per frame
    void Update()
    {
        currentTile = turnLogic.currentTile;
        text.text = "Player 1 Health: " + health;
    }

    void TakeDamage(TileManager tile)
    {
        if (tile == currentTile && health > 0)
        {
            health--;
            Debug.Log($"{gameObject.name} was hit! Health remaining: {health}");

            if (health <= 0)
            {
                gameOverScreen.SetActive(true);
                gameOverText.text = gameObject.name + " Lost! Player 2 wins, GG!";
                Debug.Log($"{gameObject.name} has run out of health! They lose.");
            }
        }
    }
}
