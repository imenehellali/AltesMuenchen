using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;

public class TaskDisplayComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _taskName;
    [SerializeField]
    private  TextMeshProUGUI _taskDescription;
    [SerializeField]
    private Image _taskProgressIcon;

    [SerializeField]
    private Sprite _inProgressTaskIcon;
    [SerializeField]
    private Sprite _finishedTaskIcon;

    [SerializeField]
    private RectTransform _taskComponentRectTransform;
    public void SetRectTransform(float _yValue) => _taskComponentRectTransform.position.Set(0f, _yValue, 0f);
    public float GetPrevTaskYPos()=> _taskComponentRectTransform.position.y;

    public void DisplaySingleTask(TaskComponent _task,RectTransform _taskContainer)
    {
        _taskComponentRectTransform = _taskContainer;
        _taskName.text
        = _task._taskName;
        _taskDescription.text
        = _task._taskDescription;
        this._taskProgressIcon.sprite
        = _task._taskFinished?_finishedTaskIcon:_inProgressTaskIcon;
    }
    public void UpdateTaskProgressIcon(TaskComponent _task) => this._taskProgressIcon.sprite
        = _task._taskFinished ? _finishedTaskIcon : _inProgressTaskIcon;

}
