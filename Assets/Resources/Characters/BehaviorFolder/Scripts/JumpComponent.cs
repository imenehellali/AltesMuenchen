using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpComponent : MonoBehaviour
{
    public static UnityAction<bool> OnJumpTriggered;

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
        OnJumpTriggered(_playerInputs.PlayerInputs.Jump1.triggered);
    }
}
