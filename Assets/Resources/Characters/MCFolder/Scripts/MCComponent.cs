using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

//this will be used to store data
public class MCComponent : MonoBehaviour
{
    //this enhabnces the special attack Light/Moon --> increase damage
    private int _sigilCount;
    private int _sigilDamage;

    //this decreases the incoming damage
    private int _defenceLevel;

    //this enhances the dagger weapon single attack --> increase damage
    private int _weaponDaggerCount;
    private int _weaponDaggerDamage;

    //this enhances the main_weapon after double Tap auto play --> increase damage
    private int _weaponDynamicCount;
    private int _weaponDynamicDamage;

    //Specifics to choose appareance of MC and data
    private int _element;
    private int _gender;
    private int _weapon;
    private string _userName = "";
    private string _UID = "";

    //this is the money used for purchase and inventory
    private int _thalersCount;

    //this enhances general GamePlay --> ++: world level increase enemies harder to kill --> track world progress
    private int _explorationLevel;
    private int _lifeSpanResistance;

    //this tracks the GamePlay and advancement of the game
    private int _currentQuest;
    private int _currentTask;

    //this tracks the position of the player and respawns it where it used to be
    private float3 _pos;



}
