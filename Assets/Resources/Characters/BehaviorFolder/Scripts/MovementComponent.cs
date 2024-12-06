using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    public static UnityAction<Vector2> OnMoveInput;

    private CustomPlayerInput _playerInputs;

    private void OnEnable()
    {
        _playerInputs.Enable();
    }

    private void OnDisable()
    {
        _playerInputs.Disable();
    }
    private void Update()
    {
        OnMoveInput(_playerInputs.PlayerInputs.Move.ReadValue<Vector2>());
    }

}
