using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuObj : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _canvasObj;
    [SerializeField] private GameObject _mcObj;
    [SerializeField] private GameObject _directionalLight;

    public static SettingsMenuObj Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        //here put the character they have chose with corresponding weapons and 
        //Here we replace the MCContainer with all MC
        _directionalLight.SetActive(true);
        _mainCamera.gameObject.SetActive(true);
        _canvasObj.GetComponent<ButtonComponent>().ShowPanel(_canvasObj);
        _mcObj.SetActive(true);
    }

    public void OnDestroy()
    {
        //if this higher priority camera is destroyed --> ill priority go to next or bug !!!!!!!
        Destroy(this.gameObject);
    }
}
