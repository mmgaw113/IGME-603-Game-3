using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnLogic : MonoBehaviour
{
    [SerializeField] private GridManager gridRef = null;
    [SerializeField] private Vector2Int startGridPos;

    private PlayerPhase phase = PlayerPhase.Inactive;
    private TileManager currentTile;

    private void Start()
    {
        if (!gridRef)
        {
            Debug.LogError($"{gameObject.name}'s turn logic was not given a grid to reference! " +
                $"Assign one in the inspector.");
        }

        gridRef.GetTile(startGridPos.x, startGridPos.y);
    }

    private void Update()
    {
        switch (phase)
        {
            case PlayerPhase.Planning:
                PlanningLogic();
                break;
            case PlayerPhase.Automated:
                break;
            case PlayerPhase.Inactive:
            default:
                break;
        }
    }

    private void PlanningLogic()
    {

    }
}