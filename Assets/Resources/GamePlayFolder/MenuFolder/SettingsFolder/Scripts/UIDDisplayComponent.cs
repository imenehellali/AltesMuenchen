using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDDisplayComponent : MonoBehaviour
{

    private void OnEnable()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text =
            "UID: "+ MCSelectionComponent.Instance.GetUID();
    }
}
