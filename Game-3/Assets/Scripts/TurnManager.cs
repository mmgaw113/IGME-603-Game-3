using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public CameraController cameraController;

    public Transform player1;
    public Transform player2;
    public Transform autoCamFocus;

    private PlayerTurnLogic play1Turn;
    private PlayerTurnLogic play2Turn;

    private GamePhase currentGamePhase;


    private void OnEnable()
    {
        PlayerTurnLogic.endTurn += OnPlayerEndTurn;
    }
    private void OnDisable()
    {
        PlayerTurnLogic.endTurn -= OnPlayerEndTurn;
    }
    private void OnPlayerEndTurn(PlayerTurnLogic enderOfTurn) { EndTurn(); }

    private void Start()
    {
        InitializePlayers();
    }

    private void Update()
    {
        TestEndofResolution();
    }

    /// <summary>
    /// Set up references and create initial game state
    /// </summary>
    private void InitializePlayers()
    {
        //Set up the initial states 
        play1Turn = player1.GetComponent<PlayerTurnLogic>();
        play2Turn = player2.GetComponent<PlayerTurnLogic>();

        InitPhase(GamePhase.Player1Planning);
    }

    public void InitPhase(GamePhase phase)
    {
        switch (phase)
        {
            case GamePhase.Player1Planning:
                currentGamePhase = GamePhase.Player1Planning;

                cameraController.SetFollowLookAtTarget(player1, player1);

                play1Turn.SetPhase(PlayerPhase.Planning);
                play2Turn.SetPhase(PlayerPhase.Inactive);
                break;

            case GamePhase.Player2Planning:
                currentGamePhase = GamePhase.Player2Planning;

                cameraController.SetFollowLookAtTarget(player2, player2);

                play1Turn.SetPhase(PlayerPhase.Inactive);
                play2Turn.SetPhase(PlayerPhase.Planning);
                break;

            case GamePhase.PlanResolution:
                currentGamePhase = GamePhase.PlanResolution;

                cameraController.SetFollowLookAtTarget(autoCamFocus, autoCamFocus);

                play1Turn.SetPhase(PlayerPhase.Automated);
                play2Turn.SetPhase(PlayerPhase.Automated);
                break;
        }
    }

    public void EndTurn()
    {
        if (currentGamePhase == GamePhase.Player1Planning)
        {
            play1Turn.ResetPos();
            InitPhase(GamePhase.Player2Planning);
        }
        else if (currentGamePhase == GamePhase.Player2Planning)
        {
            play2Turn.ResetPos();
            InitPhase(GamePhase.PlanResolution);
        }
    }

    public void TestEndofResolution()
    {
        if (currentGamePhase == GamePhase.PlanResolution
            && play1Turn.CurrentPhase == PlayerPhase.Inactive
            && play2Turn.CurrentPhase == PlayerPhase.Inactive)
        {
            InitPhase(GamePhase.Player1Planning);
        }
    }
}
