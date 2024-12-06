using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookComponent : MonoBehaviour
{
    public static UnityAction<Vector2> OnLookInput;

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
        OnLookInput(_playerInputs.PlayerInputs.Look.ReadValue<Vector2>());
    }
}
