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
    private LinkedListNode<GridDirection> node = null;

    [HideInInspector] public bool automationEnd = false;

    public static Action<PlayerTurnLogic> endTurn;
    private AttackType attack;

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

    public void SetPhase(PlayerPhase phase) => currentPhase = phase;

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
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPos();
        }

        //x attack
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Debug.Log($"Ended {gameObject.name}'s turn with x attack");
            attack = AttackType.XAttack;
            endTurn?.Invoke(this);
        }
        //+ attack
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // Debug.Log($"Ended {gameObject.name}'s turn with + attack");
            attack = AttackType.PlusAttack;
            endTurn?.Invoke(this);
        }
    }

    private void AutomatedLogic()
    {
        //Move automatically through the moves made during planning phase. Short delay between each move. Stay sync'd with
        //  other player, perhaps using a common time step, or an "automation step complete" action.
        //Use movesPlanned and repeatedly get/remove first until it's empty.

        //Debug.Log(movesPlanned.Count);

        if (!automationEnd)
        {

            foreach (GridDirection value in movesPlanned)
            {
                Coroutilities.DoAfterDelay(this, () => { TryMoveToAdjTile(value); startTile = currentTile;}, 1.0f, true);
                //Debug.Log(value);
            }
            
            automationEnd = true;
            movesPlanned.Clear();

            if (attack == AttackType.PlusAttack)
                currentTile.AttackOrthogonal(1);
            else if(attack == AttackType.XAttack)
                currentTile.AttackDiagonal(1);
        }
       

        
    }

    //  Helper Functions  //

    public void ResetPos()
    {
        transform.position = startTile.transform.position + offset;
        currentTile = startTile;
        actionsTaken = 0;
        //movesPlanned.Clear();
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