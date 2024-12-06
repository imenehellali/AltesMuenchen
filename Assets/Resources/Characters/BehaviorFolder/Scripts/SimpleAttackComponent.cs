using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleAttackComponent : MonoBehaviour
{
    public static UnityAction<bool> OnSimpleAttackTriggered;
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
        OnSimpleAttackTriggered(_playerInputs.PlayerInputs.SimpleAttack.triggered); 
    }
}
