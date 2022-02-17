using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public ActionType actionType;
    public float actionDuration;

    [Header("Move Action Fields")]
    [HideInInspector] public GridDirection direction;

    [Header("Attack Action Fields")]
    public AttackType attack;
    
    //Set the action type
    public void SetAction(ActionType action, float duration, KeyCode inputKey)
    {
        actionDuration = duration;
        switch (action)
        {
            case ActionType.Attack:
                SetAttack(inputKey);
                break;

            case ActionType.Stay:
                SetStay();
                break;
        }
    }

    public void SetAction(ActionType action, float duration, GridDirection direction)
    {
        actionDuration = duration;
        switch (action)
        {
            case ActionType.Move:
                SetMove(direction);
                break;

            case ActionType.Stay:
                SetStay();
                break;
        }
    }

    void SetMove(GridDirection dir)
    {
        actionType = ActionType.Move;
        direction = dir;
    }

    void SetAttack(KeyCode inputKey)
    {
        actionType = ActionType.Attack;

        //
        if(inputKey == KeyCode.Q)
        {
            attack = AttackType.PlusAttack;
        }
        else if(inputKey == KeyCode.E)
        {
            attack = AttackType.XAttack;
        }
    }

    void SetStay() => actionType = ActionType.Stay;
}
