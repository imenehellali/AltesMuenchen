using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MiniMapComponent : MonoBehaviour
{
    public Transform _player;
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        //Move Arrow x,z is rect same as pos only X,z
        if( _player != null)
        {
            transform.rotation = Quaternion.Euler(90f, _player.rotation.eulerAngles.y,0f);
            transform.position = new Vector3(_player.position.x, transform.position.y, _player.position.z);

        }
    }
}
