using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgressComponent : QuestComponent
{
    public QuestProgressComponent(int taskCount, int currentTask, bool questFinished, List<TaskComponent> tasks, List<QuestRewardComponent> questRewards, string questName, string questDescription) : base(taskCount, currentTask, questFinished, tasks, questRewards, questName, questDescription)
    {
    }

    private void OnEnable()
    {
        _tasks.ForEach(task => { task.OnTaskFinished += FinishATask;
            task.OnTaskStarted += StartATask;
        });
    }
    public override void FinishATask(bool _finishedTask, TaskComponent _currTask)
    {
        _currentTask++;
        if (_currentTask >= _taskCount) FinishQuest(true, this);
        else
        {
            StartATask(true, _tasks[_tasks.IndexOf(_currTask) + 1]);
        }
        
    }

    public override void FinishQuest(bool _finishQuest, QuestComponent _currQuest)
    {
        _questFinished = true;
        //Send info to currently finished quest to save/load script
        _tasks.ForEach(task=>Destroy(task.gameObject));
        //need to check the display handlers -> calling for destroyed obj's
        Destroy(this.gameObject);
    }

    public override void StartATask(bool _startTask, TaskComponent _currTask)
    {
        _currentTask =_tasks.IndexOf(_currTask);
        _tasks[_currentTask]._taskStarted = true;
        _tasks[_currentTask]._taskInProgress = true;

    }

    public override void StartQuest(bool _startQuest)
    {
        _currentTask=0;
        StartATask(true, _tasks[_currentTask]);
    }
}
