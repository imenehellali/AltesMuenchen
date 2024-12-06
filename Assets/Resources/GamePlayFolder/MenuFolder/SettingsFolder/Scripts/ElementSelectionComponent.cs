using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementSelectionComponent : MonoBehaviour
{
    public enum Element
    {
        Moon,
        Sun,
    }
    [SerializeField] 
    private Element _element;

    public static UnityAction<int> OnSelectElement;
    public void SelectElement()
    {
        if(OnSelectElement!= null)
        {
            OnSelectElement((int)_element);
        }
    }
}
