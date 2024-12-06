using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PopUpDialogueComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textPopUp;
    [SerializeField]
    private Animator _animator;
    private int _animIDStartFade;
    private int _animIDEndFade;

    private void Start()
    {

        gameObject.SetActive(true);
    }
    public void Create(string _popUpText)
    {
        _animIDStartFade = Animator.StringToHash("StartFade");
        _animIDEndFade = Animator.StringToHash("EndFade");

        _textPopUp.text = _popUpText;

        if (_animator != null)
        {
            _animator.SetBool(_animIDStartFade,true);
            _animator.SetBool(_animIDEndFade, false);
            StartCoroutine(WaitEndOfAnimation());
        }
    }

    private IEnumerator WaitEndOfAnimation()
    {
        yield return new WaitForSecondsRealtime(7f);
        _animator.SetBool(_animIDStartFade, false); 
        _animator.SetBool(_animIDEndFade, true);
        Destroy(this.gameObject);
    }



}
