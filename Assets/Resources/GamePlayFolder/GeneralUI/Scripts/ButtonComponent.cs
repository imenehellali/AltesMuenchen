using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonComponent : MonoBehaviour
{
   
    public void ShowPanel(GameObject _panel)
    {
        //Play animation if it exists to open and show panel
        //GameObject temp=GameObject.Instantiate(_panel, gameObject.GetComponentInParent<Canvas>().gameObject.transform);
        Debug.Log("clicked open panel");
        _panel.SetActive(true);
        Animator _animator= _panel.GetComponent<Animator>();
        int _animIDStartFadeIn = Animator.StringToHash("startFadeIn");
        if(_animator!=null)
        {
            Debug.Log("animator not null");

            _animator.SetBool(_animIDStartFadeIn, true);
            WaitEndOfAnimation(_animIDStartFadeIn, _panel, _animator);
        }
    }
    public void HidePanel(GameObject _panel)
    {
        Animator _animator = _panel.GetComponent<Animator>();
        int _animIDStartFadeOut = Animator.StringToHash("startFadeOut");
        if (_animator != null )
        {
            _animator.SetBool( _animIDStartFadeOut, true);
            WaitEndOfAnimation(_animIDStartFadeOut, _panel, _animator);
        }
       
    }

    public void NavigateButton(GameObject _current, GameObject _next)
    {
        _current.SetActive(false);
        _next.SetActive(true);  
    }


    IEnumerable WaitEndOfAnimation(int _animID, GameObject _panel, Animator _animator)
    {
        yield return new WaitForSecondsRealtime(3f);
        _animator.SetBool(_animID, false);
        _panel.SetActive(false);
    }
}
