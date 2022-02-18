using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnLogic : MonoBehaviour
{
    [SerializeField] private GridManager gridRef = null;
    [SerializeField] private Vector2Int startGridPos;
    [Tooltip("The player will move to the transforms of grid tiles, plus this offset.")]
    [SerializeField] private Vector3 offset;
    [SerializeField] [Min(1)] private int actionsAllowed;

    private PlayerPhase currentPhase;
    [SerializeField] private TileManager startTile;
    [SerializeField] public TileManager currentTile;
    private int actionsTaken;
    private LinkedList<GridDirection> movesPlanned;

    private bool autoLogicGate = false;

    public static Action<PlayerTurnLogic> endTurn;
    private AttackType attack;

    private bool turnOverP1;
    private bool turnOverP2;

    [SerializeField]
    private GameObject attack1VFX;
    [SerializeField]
    private GameObject attack2VFX;

    public int NumPlannedMoves { get { return movesPlanned.Count; } }

    private void Start()
    {
        if (!gridRef)
        {
            // Debug.LogError($"{gameObject.name}'s turn logic was not given a grid to reference! " +
            //    $"Assign one in the inspector.");
        }

        movesPlanned = new LinkedList<GridDirection>();
        Coroutilities.DoAfterDelayFrames(this, () => startTile = currentTile = gridRef.GetTile(startGridPos.x, startGridPos.y), 1);
    }

    public void SetPhase(PlayerPhase phase)
    {
        currentPhase = phase;
        autoLogicGate = false;
    }

    private void Update()
    {
        switch (currentPhase)
        {
            case PlayerPhase.Planning:
                PlanningLogic();
                break;
            case PlayerPhase.Automated:
                AutomatedLogic();
                break;
            case PlayerPhase.Inactive:
            default:
                break;
        }
    }

    private void PlanningLogic()
    {
        if (actionsTaken < actionsAllowed)
        {
            GridDirection? inputDir = GetInputDirection();
            if (inputDir is GridDirection validDir)
            {
                if (TryMoveToAdjTile(validDir))
                {
                    actionsTaken++;
                    movesPlanned.AddLast(validDir);
                    //  Debug.Log($"{gameObject.name} successfully moved. " +
                    //    $"Actions taken: {actionsTaken}. Actions left: {actionsAllowed - actionsTaken}.");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPos();
            movesPlanned.Clear();
        }

        //x attack
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Debug.Log($"Ended {gameObject.name}'s turn with x attack");
            attack = AttackType.XAttack;

            GameObject ps = Instantiate(attack1VFX, this.transform);
            Destroy(ps, 0.75f);

            endTurn?.Invoke(this);
        }
        //+ attack
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // Debug.Log($"Ended {gameObject.name}'s turn with + attack");
            attack = AttackType.PlusAttack;

            GameObject ps = Instantiate(attack2VFX, this.transform);
            Destroy(ps, 0.75f);

            endTurn?.Invoke(this);
        }
    }

    private void AutomatedLogic()
    {
        if (!autoLogicGate)
        {
            int i = 1;
            float delay = 0.5f;
            while (movesPlanned.Count > 0)
            {
                GridDirection dir = movesPlanned.First.Value;
                Coroutilities.DoAfterDelay(this,
                    () =>
                    {
                        TryMoveToAdjTile(dir);
                        startTile = currentTile;
                    },
                    delay * i
                );
                movesPlanned.RemoveFirst();
                i++;
            }

            Coroutilities.DoAfterDelay(this,
                () =>
                {
                    if (attack == AttackType.PlusAttack)
                        currentTile.AttackOrthogonal(1);
                    else
                        currentTile.AttackDiagonal(1);
                },
                delay * i
            );

            autoLogicGate = true;
        }
    }

    //  Helper Functions  //

    public void ResetPos()
    {
        transform.position = startTile.transform.position + offset;
        currentTile = startTile;
        actionsTaken = 0;
        // Debug.Log($"{gameObject.name} reset! Actions replenished.");
    }

    private GridDirection? GetInputDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            return GridDirection.Forward;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return GridDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            return GridDirection.Back;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            return GridDirection.Right;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Attempts to move to an adjacent tile in a given <paramref name="direction"/>.
    /// </summary>
    /// <param name="direction">The direction of the adjacent tile we want to move to.</param>
    /// <returns>Whether there was an adjacent tile in <paramref name="direction"/>/if the move was successful.</returns>
    private bool TryMoveToAdjTile(GridDirection direction)
    {
        TileManager tileInDir = currentTile.GetAdjTile(direction);
        if (tileInDir)
        {
            currentTile = tileInDir;
            transform.position = currentTile.transform.position + offset;
            return true;
        }
        else
        {
            return false;
        }
    }
}