using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UMA.CharacterSystem;

public  class MCSelectionComponent : MonoBehaviour
{

    public static MCSelectionComponent Instance { get; set; }

    private int _element;
    private int _gender;
    private int _weapon;
    private string _userName="default user name";
    private string _UID="1234567890";

    private void Awake()
    {
        if (Instance == null) { 
            Instance = this; 
        }
        else { 
            Destroy(this); 
        }
    }
    private void OnEnable()
    {
        WeaponSelectionComponent.OnSelectWeapon += SetWeapon;
        GenderSelectionComponent.OnSelectGender += SetGender;
        ElementSelectionComponent.OnSelectElement += SetElement;
        UserNameInputComponent.OnValuechanged += SetUserName;
    }

    public void SetElement(int _element)
    {
        this._element = _element;
        Debug.Log(this._element);
    }
    public void SetGender(int _gender)
    {
        this._gender = _gender;
       /* switch(this._gender)
        {
            case 0:
                _UMADC.ChangeRace("HumanFemale");
                _UMADC.loadFilename = "FemalePreset";
                _UMADC.DoLoad();
                break;
            case 1:
                _UMADC.ChangeRace("HumanMale");
                _UMADC.loadFilename = "MalePreset";
                _UMADC.DoLoad();
                break;
        }*/
        Debug.Log(this._gender);
    }
    public void SetWeapon(int _weapon)
    {
        this._weapon = _weapon;
        Debug.Log(this._weapon);
    }
    public void SetUserName(string _input)
    {
        _userName = _input;
        Debug.Log(this._userName);
    }
    public int GetGender()
    {
        return _gender;
    }
    public string GetUserName()
    {
        return _userName;
    }
    public string GetUID()
    {
        return _UID;
    }
    private void SetUID()
    {
        //Randomly generate new UID for each new player and save permnently 
    }
}
