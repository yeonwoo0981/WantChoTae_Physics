using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInput PlayerInput { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        PlayerMovement.SetXMove(PlayerInput.MoveDir.x);
    }
}
