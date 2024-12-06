using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserNameDisplayComponent : MonoBehaviour
{
    private void OnEnable()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "User namer:  " + MCSelectionComponent.Instance.GetUserName();
    }
}
