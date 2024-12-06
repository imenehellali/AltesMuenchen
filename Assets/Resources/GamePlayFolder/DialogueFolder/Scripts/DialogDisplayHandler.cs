using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogDisplayHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textDialogue;
    [SerializeField]
    private TextMeshProUGUI _textDialogueOrig;
    [SerializeField]
    private TextMeshProUGUI _textSpeaker;
    [SerializeField]
    private Animator _animator;

    private int _animIDFadeInOnStart;
    private int _animIDFadeOutBeforeDestroy;

    private DialogHandler _DHandler;
    private bool _enableTranslation = false;

    public void Init(DialogHandler _DHandler, bool _isNativeLanguage)
    {
        this._DHandler = _DHandler;
        _enableTranslation = _isNativeLanguage;
        if(_enableTranslation )
        {
            _textDialogueOrig.gameObject.SetActive(true);
        }
    }
    private void OnEnable()
    {
        DialogHandler._DialogEnded += EndDialogue;
        DialogHandler._DialogPlayNext += PlayNextDialogueSegment;
        DialogHandler._DialogStarted += StartDialogue;
    }
    private void Awake()
    {
        _animIDFadeInOnStart = Animator.StringToHash("FadeInOnStart");
        _animIDFadeOutBeforeDestroy = Animator.StringToHash("FadeOutBeforeDestroy");
    }

    private void EndDialogue(bool _dialogueEnd)
    {
        if (_dialogueEnd)
        {
            _animator.SetBool(_animIDFadeOutBeforeDestroy, true);
            StartCoroutine(WaitEndOfFadeOut());
            //--> how to use taskComponent here the TaskFinished?? who calls who?
            //Update Task / questr make a listener for this variable
        }
    }

    private void PlayNextDialogueSegment(bool _dialogueIsPlaying)
    {
        if(_dialogueIsPlaying)
        {
            DisplayDialogue(_DHandler);
        }
    }

    private void StartDialogue(bool _dialogueStart)
    {
        if (_dialogueStart)
        {
            this.gameObject.SetActive(true);
            _DHandler._dialogueIsPlaying = true;

            _animator.SetBool(_animIDFadeInOnStart, true);

            DisplayDialogue(_DHandler);
        }
    }
    private void DisplayDialogue(DialogHandler _DHandler)
    {
        Debug.Log("saying:    "+ _DHandler._dialogues[_DHandler._dialogueIdx].dialog);
        Debug.Log("speakerName:   " + _DHandler._dialogues[_DHandler._dialogueIdx].name);
        _textDialogue.text = _DHandler._dialogues[_DHandler._dialogueIdx].dialog;
        _textDialogueOrig.text = _DHandler._dialogues[_DHandler._dialogueIdx].dialog;
        _textSpeaker.text = _DHandler._dialogues[_DHandler._dialogueIdx].name;
    }
    private IEnumerator WaitEndOfFadeOut()
    {
        yield return new WaitForSecondsRealtime(2f);
        _animator.SetBool(_animIDFadeOutBeforeDestroy, false);
        Destroy(this.gameObject);
    }

    public void PlayNextButton()
    {
        _DHandler.PlayNextDialogueSegment();
    }
}
