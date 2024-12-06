using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestRewardComponent : MonoBehaviour
{

    public enum Reward
    {
        Sigil,
        WeaponDagger,
        WeaponAxe,
        WeaponSword,
        Exploration,
        Thales,
    }

    [HideInInspector]
    public int _rewardCount;
    [HideInInspector]
    public Reward _rewardType;

    public QuestRewardComponent(Reward reward, int count)
    {
        _rewardCount = count;
        _rewardType = reward;
    }
}
