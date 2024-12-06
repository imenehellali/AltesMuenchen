using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public Animator _animator;
    public GameObject _canvas;
   // public Camera _introCamera;

    private int _animationIDShowStartGame;
    private int _animationIDStartGame;

    private bool _startGame=false;
    private bool _startShowGame = true;
    private bool _applyRoot = true;

    private AsyncOperation _mainOP;
    private void Awake()
    {
    }
    private void Start()
    {
        _animationIDShowStartGame = Animator.StringToHash("ShowStartGame");
        _animator.SetBool(_animationIDShowStartGame, false);
        _animationIDStartGame = Animator.StringToHash("StartGame");
        _animator.SetBool(_animationIDStartGame, false);

        _mainOP = SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);

    }
    private void Update()
    {  
        if(_mainOP.isDone)
        {
            _animator.SetBool(_animationIDShowStartGame,true);
        }
    }
    public void SatrtGame()
    {
        SceneManager.UnloadSceneAsync(0);
    }
   
}
