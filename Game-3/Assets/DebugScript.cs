using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public CameraController cameraController;

    public Transform player1;
    public Transform player2;

    private bool player1Active = true;

    // Update is called once per frame
    private void Start()
    {
        cameraController.SetFollowLookAtTarget(player1, player1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            player1Active = !player1Active;

            if (player1Active)
            {
                cameraController.SetFollowLookAtTarget(player2, player2);
            }
            else
            {
                cameraController.SetFollowLookAtTarget(player1, player1);
            }
        }
    }
}
