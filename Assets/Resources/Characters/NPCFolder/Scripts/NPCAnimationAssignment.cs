using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCAnimationAssignment : MonoBehaviour
{
    public enum AnimationType
    {
    Sit,
    Buff,
    HipedSpeech,
    SliceBread,
    Mix,
    Fish,
    Sew,
    Hammer,
    Conduct,
    Craft,
    chopWood,
    ChopWood1,
    ChopWood2,
    PickUp,
    PickUpNPutOnshelf,
    Kneel,
    SandDrink,
    Stunned,
    Selling1,
    Selling2,
    Selling3,
    Walk,
}

    public AnimationType _animType;


    private Animator _animator;
    private int _animIDSit;
    private int _animIDBuff;
    private int _animIDHipedSpeech;
    private int _animIDSliceBread;
    private int _animIDMix;
    private int _animIDFish;
    private int _animIDSew;
    private int _animIDHammer;
    private int _animIDConduct;
    private int _animIDCraft;
    private int _animIDChopWood;
    private int _animIDChopWood1;
    private int _animIDChopWood2;
    private int _animIDPickUp;
    private int _animIDPickUpNPutOnshelf;
    private int _animIDKneel;
    private int _animIDStandDrink;
    private int _animIDStunned;
    private int _animIDSelling1;
    private int _animIDSelling2;
    private int _animIDSelling3;
    private int _animIDWalk;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _animIDSit = Animator.StringToHash("sit");
        _animIDBuff = Animator.StringToHash("Buff");
        _animIDHipedSpeech = Animator.StringToHash("HipedSpeech");
        _animIDSliceBread = Animator.StringToHash("SliceBread");
        _animIDMix = Animator.StringToHash("Mix");
        _animIDFish = Animator.StringToHash("Fish");
        _animIDSew = Animator.StringToHash("Sew");
        _animIDHammer = Animator.StringToHash("Hammer");
        _animIDConduct = Animator.StringToHash("Conduct");
        _animIDCraft = Animator.StringToHash("Craft");
        _animIDChopWood = Animator.StringToHash("chopWood");
        _animIDChopWood1 = Animator.StringToHash("chopWood1");
        _animIDChopWood2 = Animator.StringToHash("chopWood2");
        _animIDPickUp = Animator.StringToHash("pickUp");
        _animIDPickUpNPutOnshelf = Animator.StringToHash("pickUpNPutOnShelf");
        _animIDKneel = Animator.StringToHash("kneel");
        _animIDStandDrink = Animator.StringToHash("standDrink");
        _animIDStunned = Animator.StringToHash("Stunned");
        _animIDSelling1 = Animator.StringToHash("Selling1");
        _animIDSelling2 = Animator.StringToHash("Selling2");
        _animIDSelling3= Animator.StringToHash("Selling3");
        _animIDWalk = Animator.StringToHash("Walk");
    }
    void Start()
    {
        
        SetAnim();
    }

    private void SetAnim()
    {
        switch(_animType)
        {
            case AnimationType.Sit:
                _animator.SetBool(_animIDSit, true);
                break;
            case AnimationType.Buff:
                _animator.SetBool(_animIDBuff, true);
                break;
            case AnimationType.HipedSpeech:
                _animator.SetBool(_animIDHipedSpeech, true);
                break;
            case AnimationType.SliceBread:
                _animator.SetBool(_animIDSliceBread, true);
                break;
            case AnimationType.Mix:
                _animator.SetBool(_animIDMix, true);
                break;
            case AnimationType.Fish:
                _animator.SetBool(_animIDFish, true);
                break;
            case AnimationType.Sew:
                _animator.SetBool(_animIDSew, true);
                break;
            case AnimationType.Hammer:
                _animator.SetBool(_animIDHammer, true);
                break;
            case AnimationType.Conduct:
                _animator.SetBool(_animIDConduct, true);
                break;
            case AnimationType.Craft:
                _animator.SetBool(_animIDCraft, true);
                break;
            case AnimationType.chopWood:
                _animator.SetBool(_animIDChopWood, true);
                break;
            case AnimationType.ChopWood1:
                _animator.SetBool(_animIDChopWood1, true);
                break;
            case AnimationType.ChopWood2:
                _animator.SetBool(_animIDChopWood2, true);
                break;
            case AnimationType.PickUp:
                _animator.SetBool(_animIDPickUp, true);
                break;
            case AnimationType.PickUpNPutOnshelf:
                _animator.SetBool(_animIDPickUpNPutOnshelf, true);
                break;
            case AnimationType.Kneel:
                _animator.SetBool(_animIDKneel, true);
                break;
            case AnimationType.SandDrink:
                _animator.SetBool(_animIDStandDrink, true);
                break;
            case AnimationType.Stunned:
                _animator.SetBool(_animIDStunned, true);
                break;
            case AnimationType.Selling1:
                _animator.SetBool(_animIDSelling1, true);
                break;
            case AnimationType.Selling2:
                _animator.SetBool(_animIDSelling2, true);
                break;
            case AnimationType.Selling3:
                _animator.SetBool(_animIDSelling3, true);
                break;
            case AnimationType.Walk:
                _animator.SetBool(_animIDWalk, true);
                break;
        }
    }
}
