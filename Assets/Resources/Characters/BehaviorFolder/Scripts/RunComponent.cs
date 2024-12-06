using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RunComponent : MonoBehaviour
{

    public static UnityAction<bool> OnRunTriggered;
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
        OnRunTriggered(_playerInputs.PlayerInputs.Sprint.triggered);
    }
}
