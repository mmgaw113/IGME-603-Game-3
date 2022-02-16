using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridDirection
{
    LeftForward,
    Forward,
    RightForward,
    Right,
    RightBack,
    Back,
    LeftBack,
    Left
}

public enum PlayerPhase
{
    Inactive,
    Planning,
    Automated
}

public enum GamePhase
{
    Player1Planning,
    Player2Planning,
    PlanResolution
}