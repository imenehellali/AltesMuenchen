using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuestsHandler :MonoBehaviour
{ 
    public int _questCount;
    public int _currQuestID;
    public GameObject _singleQuestPref;
    public RectTransform _questContainer;
    public RectTransform _taskContainer;
    public RectTransform _rewardContainer;


    private GameObject _currQuest;
    


    private void Awake()
    {
        TaskComponent _task1 = new ExploreTaskComponent("Yggdrasil the origin of 9 worlds", "Wait until 3am and head to the tree of life Yggdrasil, connecting all the worlds, to exit the tribe!", false, false, false);
        QuestRewardComponent _reward1 = new QuestSingleRewardHandler(QuestRewardComponent.Reward.Sigil, 3);
        QuestRewardComponent _reward2 = new QuestSingleRewardHandler(QuestRewardComponent.Reward.WeaponAxe, 2);
        QuestRewardComponent _reward3 = new QuestSingleRewardHandler(QuestRewardComponent.Reward.Thales, 500);
        QuestRewardComponent _reward4 = new QuestSingleRewardHandler(QuestRewardComponent.Reward.Exploration, 40);

        List<TaskComponent> _quest1Tasks=new List<TaskComponent>();
        List<QuestRewardComponent> _quest1Rewards = new List<QuestRewardComponent>();

        _quest1Tasks.Add(_task1);
        _quest1Rewards.Add(_reward1);
        _quest1Rewards.Add(_reward2);
        _quest1Rewards.Add(_reward3);
        _quest1Rewards.Add(_reward4);


        QuestProgressComponent _quest1 = new QuestProgressComponent(1, 0, false, _quest1Tasks, _quest1Rewards, "Escape the matrix and discover yourself", "After your coming-of-age ceremony you decide to run away from the tribe to explore a new culture, the new build city Munich by the Wittelsbachs' Dynasty");
        _currQuest = Instantiate(_singleQuestPref,_questContainer);
        _currQuest.GetComponent<SingleQuestDisplayHandler>().Init(_quest1,_rewardContainer,_taskContainer);
        
        
    }
    private void Start()
    {
        _currQuest.GetComponent<SingleQuestDisplayHandler>().DisplayQuest();
    }
}
