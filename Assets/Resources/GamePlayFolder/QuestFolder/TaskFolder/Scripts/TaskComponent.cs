using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TaskComponent : MonoBehaviour
{
    public string _taskName;
    public string _taskDescription;

    public bool _taskFinished;
    public bool _taskStarted;
    public bool _taskInProgress;

    public  UnityAction<bool,TaskComponent> OnTaskFinished; //used to update quest
    public  UnityAction<bool, TaskComponent> OnTaskStarted; //used to uodate quest
    public  UnityAction<bool> OnTaskInProgress; //used to update Icon
    public TaskComponent(string taskName, string taskDescription, bool taskFinished, bool taskStarted, bool taskInProgress)
    {
        _taskName = taskName;
        _taskDescription = taskDescription;
        _taskFinished = taskFinished;
        _taskStarted = taskStarted;
        _taskInProgress = taskInProgress;
    }

    public void TaskFinished() { if (OnTaskFinished != null) OnTaskFinished(_taskFinished,this); }
    public void TaskStarted() { if (OnTaskStarted != null) OnTaskStarted(_taskStarted, this); }

    public void TaskInProgress(){ if (OnTaskInProgress != null) OnTaskInProgress(_taskInProgress);}

}
