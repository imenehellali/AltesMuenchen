using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Unity.VisualScripting;

public abstract class DialogComponent : MonoBehaviour
{
    public struct NameDialog{

        public string name;
        public string dialog;

        public NameDialog(string name, string dialog)
        {
            this.name = name;
            this.dialog = dialog;
        }
    }
    
    [HideInInspector]
    public List<NameDialog> _dialogues;
    [HideInInspector]
    public bool _dialogueStart;
    [HideInInspector]
    public bool _dialogueEnd;
    [HideInInspector]
    public bool _dialogueIsPlaying;
    [HideInInspector]
    public int _dialogueIdx;

    
    public DialogComponent(List<NameDialog> _dialogues,
        bool _dialogueStart, bool _dialogueEnd, bool _dialogueIsPlaying)
    {
        this._dialogues = _dialogues;
        this._dialogueStart = _dialogueStart;
        this._dialogueEnd = _dialogueEnd;
        this._dialogueIsPlaying = _dialogueIsPlaying;
        _dialogueIdx = 0;
    }
    public void SetDialogComponent(DialogComponent _component)
    {
        this._dialogues = _component._dialogues;
        this._dialogueStart = _component._dialogueStart;
        this._dialogueEnd = _component._dialogueEnd;
        this._dialogueIsPlaying = _component._dialogueIsPlaying;
    }
    public abstract void PlayNextDialogueSegment();
    public abstract void StartDialogue();
}
