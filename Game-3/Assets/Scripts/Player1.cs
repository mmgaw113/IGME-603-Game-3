using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public int health = 3;
    public int turnNumber = 1;

    PlayerTurnLogic turnLogic;

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
        if (tile == turnLogic.currentTile)
        {
            health--;
            Debug.Log($"{gameObject.name} was hit! Health remaining: {health}");

            if (health <= 0)
            {
                Debug.Log($"{gameObject.name} has run out of health! They lose.");
            }
        }
    }
}
