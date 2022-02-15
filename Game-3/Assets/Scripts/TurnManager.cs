using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public CameraController cameraController;

    public Transform player1;
    public Transform player2;

    private PlayerTurnLogic play1Turn;
    private PlayerTurnLogic play2Turn;

    private bool player1Active = true;

    GameState gameState;

    // Update is called once per frame
    private void Start()
    {
        InitializePlayers();
    }

    void Update()
    {

    }

    /// <summary>
    /// Set up references and create initial game state
    /// </summary>
    private void InitializePlayers()
    {
        //Set up the initial states 
        play1Turn = player1.GetComponent<PlayerTurnLogic>();
        play2Turn = player2.GetComponent<PlayerTurnLogic>();

        ChangeGameStateUpdate(GameState.Player1Turn);
    }

    public void ChangeGameStateUpdate(GameState state)
    {
        switch (state)
        {
            case GameState.Player1Turn:
                gameState = GameState.Player1Turn;

                cameraController.SetFollowLookAtTarget(player1, player1);

                play1Turn.SetPhase(PlayerPhase.Planning);
                play2Turn.SetPhase(PlayerPhase.Inactive);
                break;

            case GameState.Player2Turn:
                gameState = GameState.Player2Turn;

                cameraController.SetFollowLookAtTarget(player2, player2);

                play1Turn.SetPhase(PlayerPhase.Inactive);
                play2Turn.SetPhase(PlayerPhase.Planning);
                break;

            case GameState.Automation:
                gameState = GameState.Automation;

                cameraController.SetFollowLookAtTarget(player2, player2);

                play1Turn.SetPhase(PlayerPhase.Automated);
                play2Turn.SetPhase(PlayerPhase.Automated);
                break;
        }
    }

    public void EndTurn()
    {
        if (gameState == GameState.Player1Turn)
        {
            play1Turn.ResetPos();
            ChangeGameStateUpdate(GameState.Player2Turn);            
        }
        else if (gameState == GameState.Player2Turn)
        {
            play2Turn.ResetPos();
            ChangeGameStateUpdate(GameState.Automation);
        }          
    }
}
