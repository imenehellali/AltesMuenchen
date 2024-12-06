using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenderSelectionComponent : MonoBehaviour
{   
    public enum Gender
    {
        Female,
        Male,
    }
    [SerializeField]
    private Gender _gender;

    public static UnityAction<int> OnSelectGender;
    public void SelectGender()
    {
        if(OnSelectGender!=null)
        {
            OnSelectGender((int)_gender);
        }
    }
}
