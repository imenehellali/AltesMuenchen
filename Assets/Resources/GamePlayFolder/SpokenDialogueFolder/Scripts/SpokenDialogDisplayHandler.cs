using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpokenDialogDisplayHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _speakerContainer;
    [SerializeField]
    private TextMeshProUGUI _OVDialogContainer;
    [SerializeField]
    private TextMeshProUGUI _SubDialogContainer;
    [SerializeField, HideInInspector]
    private Animator _animator;
    [SerializeField, HideInInspector]
    private AudioSource _audioSource;

    private int _animIDPopUp;
    private int _animIDStartFadeInNOut;
    private int _animIDPopOut;


    private void OnEnable()
    {
        SpokenDialogBehaviorHandler._EndCurrentDialogueSegment += DisplayNextDialogSegment;
        SpokenDialogBehaviorHandler._EndSpokenDialogue += EndSpokenDialogue;
        SpokenDialogBehaviorHandler._StartSpokenDialogue += StartDialogue;
    }

    private void Start()
    {
        this._animator = GetComponent<Animator>();
        this._audioSource = GetComponent<AudioSource>();

        _animIDPopUp = Animator.StringToHash("PopUp");
        _animIDStartFadeInNOut = Animator.StringToHash("StartFadeInNOut");
        _animIDPopOut = Animator.StringToHash("PopOut");
    }

    private void StartDialogue(bool _triggered, SpokenDialogComponent _dialogue)
    {

        if (_triggered)
        {

            _animator.SetBool(_animIDStartFadeInNOut, false);
            _animator.SetBool(_animIDPopOut, false);
            _animator.SetBool(_animIDPopUp, true);
            StartCoroutine(WaitEndOfPopUpanimation(_dialogue));
           
        }
    }
    private void DisplayNextDialogSegment(bool _triggered, SpokenDialogComponent _dialogue)
    {
        if(_triggered)
        {
            _dialogue._SubDialogueIndex++;

            if (_dialogue._SubDialogueIndex >= _dialogue._SubDialogueSegments.Count)
            {
                _dialogue.SendMessage("DialogEnded");
            }
            else
            { 
                _animator.SetBool(_animIDStartFadeInNOut, true);

                _speakerContainer.text = _dialogue._SpeakerListOrdered[_dialogue._SubDialogueIndex].ToString();
                _OVDialogContainer.text = _dialogue._OVDialogueSegments[_dialogue._SubDialogueIndex].ToString();
                _SubDialogContainer.text = _dialogue._SubDialogueSegments[_dialogue._SubDialogueIndex].ToString();

                _audioSource.PlayOneShot(_dialogue._OVAudioSegments[_dialogue._SubDialogueIndex]);
                StartCoroutine(WaitEndOfDialogSegment(_dialogue));
            }
        }

    }

    private void EndSpokenDialogue(bool _triggered, SpokenDialogComponent _dialogue)
    {
        if(_triggered)
        {

            _animator.SetBool(_animIDPopOut, true);

            _dialogue._SubDialogueIndex=0;
            StartCoroutine(WaitEndOfAnimation());
        }
    }



    IEnumerator WaitEndOfDialogSegment(SpokenDialogComponent _dialogue)
    {
        yield return new WaitForSecondsRealtime(_dialogue._CurrAudioSegmentLength = _dialogue._OVAudioSegments[_dialogue._SubDialogueIndex].length);
        _animator.SetBool(_animIDStartFadeInNOut, false);

        _dialogue.SendMessage("CurrDialogSegmentEnded"); 
        
    }
    IEnumerator WaitEndOfPopUpanimation(SpokenDialogComponent _dialogue)
    {
        yield return new WaitForSecondsRealtime(3f);
        _animator.SetBool(_animIDPopUp, false);
        _speakerContainer.text = _dialogue._SpeakerListOrdered[_dialogue._SubDialogueIndex].ToString();
        _OVDialogContainer.text = _dialogue._OVDialogueSegments[_dialogue._SubDialogueIndex].ToString();
        _SubDialogContainer.text = _dialogue._SubDialogueSegments[_dialogue._SubDialogueIndex].ToString();
        _audioSource.PlayOneShot(_dialogue._OVAudioSegments[_dialogue._SubDialogueIndex]);

        StartCoroutine(WaitEndOfDialogSegment(_dialogue));
    }
    IEnumerator WaitEndOfAnimation()
    {
        yield return new WaitForSecondsRealtime(1f);
        _animator.SetBool(_animIDPopOut, false);
        Destroy(this.gameObject); 

    }
}
