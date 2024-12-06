using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSingleRewardHandler : QuestRewardComponent
{
    public QuestSingleRewardHandler(Reward reward, int count) : base(reward, count)
    {
    }

    public QuestRewardComponent ConsumeReward()
    {
        return this;
    }
}
