using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogHandler : DialogComponent
{
    
    public static UnityAction<bool> _DialogEnded;
    public static UnityAction<bool> _DialogStarted;
    public static UnityAction<bool> _DialogPlayNext;
    public DialogHandler(List<NameDialog> _dialogues, bool _dialogueStart, bool _dialogueEnd, bool _dialogueIsPlaying) : base(_dialogues, _dialogueStart, _dialogueEnd, _dialogueIsPlaying)
    {
    }
    public override void PlayNextDialogueSegment()
    {
        _dialogueIdx++;

        if (_dialogueIdx <_dialogues.Count)
        {

            _DialogPlayNext(true);
        }
        else
        {

            _dialogueEnd = true;
            _DialogEnded(true);
        }
    }

    public override void StartDialogue()
    {
        _dialogueStart= true;//make this the boolean hat is filled with events
        if(_dialogueStart && _DialogStarted!=null)
        {
            _DialogStarted(true);
        }
    }

}
