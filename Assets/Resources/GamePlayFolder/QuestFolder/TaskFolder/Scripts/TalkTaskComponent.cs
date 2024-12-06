using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTaskComponent : TaskComponent
{
    private DialogComponent _taskDialogue;
    public TalkTaskComponent(string taskName, string taskDescription, bool taskFinished, bool taskStarted, bool taskInProgress) : base(taskName, taskDescription, taskFinished, taskStarted, taskInProgress)
    {
    }
}
