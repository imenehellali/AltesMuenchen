using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExploreTaskComponent : TaskComponent
{
    public ExploreTaskComponent(string taskName, string taskDescription, bool taskFinished, bool taskStarted, bool taskInProgress) : base(taskName, taskDescription, taskFinished, taskStarted, taskInProgress)
    {
    }

    private bool _timeWindowReached = false;
    private bool _destinationReched = false;
    public Vector3 _targetPos=Vector3.zero;



}
