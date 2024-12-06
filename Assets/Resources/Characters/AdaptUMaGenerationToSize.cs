using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptUMaGenerationToSize : MonoBehaviour
{
    private CapsuleCollider _cc;
    private float _height=2f;
    private float _radius=.2f;
    private float _centerY=.35f;
    void Start()
    {
        _cc = gameObject.GetComponent<CapsuleCollider>();
        if( _cc != null )
        {
            _cc.center=new Vector3(0,_centerY,0);
            _cc.height = _height;
            _cc.radius = _radius;
        }
    }
    private void Update()
    {
        if(_cc != null && _cc.center.y!=_centerY)
        {
            Debug.Log("Updating from update");
            _cc.center = new Vector3(0, _centerY, 0);
            _cc.height = _height;
            _cc.radius= _radius;    
        }
        else if(_cc == null)
        {
            _cc = gameObject.GetComponent<CapsuleCollider>();
        }
    }
}
