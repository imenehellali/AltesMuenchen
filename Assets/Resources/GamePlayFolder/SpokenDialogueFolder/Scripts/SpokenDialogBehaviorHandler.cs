using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpokenDialogBehaviorHandler : SpokenDialogComponent
{

    public SpokenDialogBehaviorHandler(List<string> oVDialogueSegments, List<string> subDialogueSegments, List<string> speakerListOrdered, List<AudioClip> oVAudioSegments, int subDialogueIndex) : base(oVDialogueSegments, subDialogueSegments,speakerListOrdered, oVAudioSegments, subDialogueIndex)
    {
    }


    public void StartDialog()
    {
        if (_StartSpokenDialogue != null)
        {
            _StartSpokenDialogue(true, this);
        }
    }
    public void CurrDialogSegmentEnded()
    {
        if(_EndCurrentDialogueSegment!= null)
        {
            _EndCurrentDialogueSegment(true,this);
        }
    }

    public void DialogEnded()
    {
        if (_EndSpokenDialogue != null)
        {
            _EndSpokenDialogue(true, this);
        }
    }
}
