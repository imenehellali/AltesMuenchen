using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SpokenDialogComponent : MonoBehaviour
{
    public List<string> _OVDialogueSegments;
    public List<string> _SubDialogueSegments;
    public List<string> _SpeakerListOrdered;
    public List<AudioClip> _OVAudioSegments;

    [HideInInspector]
    public float _CurrAudioSegmentLength = 0;
    [HideInInspector]
    public int _SubDialogueIndex = 0;

    [HideInInspector]
    public static UnityAction<bool,SpokenDialogComponent> _EndSpokenDialogue;
    public static UnityAction<bool,SpokenDialogComponent> _EndCurrentDialogueSegment;
    public static UnityAction<bool, SpokenDialogComponent> _StartSpokenDialogue;

    public SpokenDialogComponent(List<string> oVDialogueSegments, List<string> subDialogueSegments, List<string> speakerListOrdered, List<AudioClip> oVAudioSegments, int subDialogueIndex)
    {

        _OVDialogueSegments = oVDialogueSegments;
        _SubDialogueSegments = subDialogueSegments;
        _SpeakerListOrdered= speakerListOrdered;

        _OVAudioSegments = oVAudioSegments;
        _SubDialogueIndex = subDialogueIndex;
    }
}
