using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSelectionComponent : MonoBehaviour
{
    public enum Weapon
    {
        AxenDagger,
        SwordnDagger,
    }
    [SerializeField]
    private Weapon _weapon;

    public static UnityAction<int> OnSelectWeapon;
    public void SelectWeapon()
    {
        if(OnSelectWeapon != null)
        {
            OnSelectWeapon((int)_weapon);
        }
    }
}
