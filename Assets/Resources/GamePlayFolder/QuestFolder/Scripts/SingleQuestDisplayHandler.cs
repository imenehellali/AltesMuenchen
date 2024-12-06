using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class SingleQuestDisplayHandler : MonoBehaviour
{
   
    [SerializeField]
    private TextMeshProUGUI _questName;
    [SerializeField]
    private TextMeshProUGUI _questDescription;

    [SerializeField]
    private GameObject _singleTaskPrefab;
    private GameObject[] _tasks;
    private List<TaskDisplayComponent> _displayTasks;

    private RectTransform _rewardViewPort;
    private RectTransform _taskViewPort;

    [SerializeField]
    private GameObject _singleRewardPrefab;
    private GameObject[] _rewards;
    private List<SingleQuestRewardDisplayComponent> _displayRewards;

    private QuestProgressComponent _quest;
    public void Init(QuestProgressComponent _quest, RectTransform _rewardRect, RectTransform _taskRect)
    {
        this._quest = _quest;
        _rewardViewPort = _rewardRect;
        _taskViewPort = _taskRect;
        _tasks=new GameObject[_quest._tasks.Count];
        _rewards=new GameObject[_quest._QuestRewards.Count]; 
    }

    public void DisplayQuest()
    {
        _questName.text =_quest. _QuestName;
        _questDescription.text = _quest._QuestDescription;

        _displayTasks = new List<TaskDisplayComponent>();
        _displayRewards= new List<SingleQuestRewardDisplayComponent>();

        int _i = 0;
        foreach (var task in _quest._tasks) 
        {
            _tasks[_i] = Instantiate(_singleTaskPrefab, _taskViewPort);
            _tasks[_i].GetComponent<TaskDisplayComponent>().DisplaySingleTask(_quest._tasks[_i],_taskViewPort);

            _displayTasks.Add(_tasks[_i].GetComponent<TaskDisplayComponent>());

            _displayTasks[_i].SetRectTransform(_i > 0?_displayTasks[_i-1].GetPrevTaskYPos()-250f-80f:-60f);
            _i++;
        }
        _i = 0;
        foreach (var reward in _quest._QuestRewards)
        {
            _rewards[_i] = Instantiate(_singleRewardPrefab, _rewardViewPort);
            _rewards[_i].GetComponent<SingleQuestRewardDisplayComponent>().DisplayReward(reward._rewardType, reward._rewardCount);

            _displayRewards.Add(_rewards[_i].GetComponent<SingleQuestRewardDisplayComponent>());

            float _XPos = _i > 0 ? _displayRewards[_i - 1].GetPrevRewardPos() + 200f : 100f;
            _rewards[_i].GetComponent<SingleQuestRewardDisplayComponent>().SetRectTransform(_XPos,_singleRewardPrefab.GetComponent<RectTransform>().localPosition.y, _singleRewardPrefab.GetComponent<RectTransform>().localPosition.z);
            _i++;
        }
    }


}
