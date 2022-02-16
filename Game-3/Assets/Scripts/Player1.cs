using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public int health = 3;
    public Transform playerPosition;
    private void Update()
    {
        Debug.Log(transform.position.x);
    }
}
