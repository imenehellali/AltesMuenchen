using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestComponent : MonoBehaviour
{
    public int _taskCount;
    public int _currentTask;
    public bool _questFinished;

    [HideInInspector]
    public List<TaskComponent> _tasks;
    [HideInInspector]
    public List<QuestRewardComponent> _QuestRewards;

    public string _QuestName;
    public string _QuestDescription;

    public QuestComponent(int taskCount, int currentTask, bool questFinished, List<TaskComponent> tasks, List<QuestRewardComponent> questRewards, string questName, string questDescription)
    {
        _taskCount = taskCount;
        _currentTask = currentTask;
        _questFinished = questFinished;
        _tasks = tasks;
        _QuestRewards = questRewards;
        _QuestName = questName;
        _QuestDescription = questDescription;
    }

    public abstract void FinishATask(bool _taskFinishedbool, TaskComponent _currtask);
    public abstract void StartATask(bool _taskStarted, TaskComponent _currTask);
    public abstract void FinishQuest(bool _questFinished,QuestComponent _currQuest);
    public abstract void StartQuest(bool _questStarted);

}
