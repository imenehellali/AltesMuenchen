using System.Collections;
using System.Collections.Generic;
using UMA.CharacterSystem;
using UnityEngine;

public class NPCComponent : MonoBehaviour
{
    public enum NPCType
    {
        Seller_M_0,
        Seller_M_1,
        Seller_F_0,
        Seller_F_1,
        Seller_F_2,
        Seller_F_3,
        BlackSmith,
        Walker_F_0,
        Walker_M_0,
        Eater_F_0,
        Eater_F_1,
        Eater_F_2,
        Eater_F_3,
        Eater_F_4,
        Eater_M_0,
        Eater_M_1,
        Eater_M_2,
        Eater_M_3,
        Eater_M_4,
    }
    public NPCType _type;
    void Awake()
    {
    }

    private void Start()
    {
    }

}
