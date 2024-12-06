using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserNameInputComponent : MonoBehaviour
{
    public static UnityAction<string> OnValuechanged;
    private void OnEnable()
    {
        if(MCSelectionComponent.Instance.GetUserName()!="")
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text =
            MCSelectionComponent.Instance.GetUserName();
    }
    public void InputedText()
    {
        string _input=this.GetComponent<TMP_InputField>().text;
        if(OnValuechanged!=null && _input!="")
        {
            OnValuechanged(_input);
        }
    }
}
