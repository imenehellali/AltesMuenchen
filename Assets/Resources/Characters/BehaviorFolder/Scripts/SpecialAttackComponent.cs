using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialAttackComponent : MonoBehaviour
{
    public static UnityAction<bool> OnSpecialAttackTriggered;
    // in real seconds this ammount deceases with the enhancement of Mc

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
       OnSpecialAttackTriggered(_playerInputs.PlayerInputs.SpecialAttack.triggered);
    }


    //Add function to decrease the cool off time that blocks the 
    //special attack button and releases it after cool off 
    //may use animation here for overlay and blocking raycast
}
