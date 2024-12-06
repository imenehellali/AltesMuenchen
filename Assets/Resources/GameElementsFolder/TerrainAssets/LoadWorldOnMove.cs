using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.AI;

public class LoadWorldOnMove : MonoBehaviour
{

    public float _visibleRange = 100f;
    public float _focalLength = 30f;
    public Vector2[] _worldScenesStartVisible; //Ordered Same as worldScenes
    private AsyncOperation[] _worldScenesAO;
    public AudioSource[] _ambientaudiosources=new AudioSource[5];

    private Vector2 _position; //x: is X pos of player Y: is Z pos of player

    private AsyncOperation _loadingMCTown;
    private AsyncOperation _loadingTownSquare;
    private AsyncOperation _loadingMCTownNPC;
    private AsyncOperation _loadingTownSquareNPC;
    public static LoadWorldOnMove Instance { get; set; }
    public bool _introScenePlaying=false;
    

    private void Awake()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        _worldScenesAO =new AsyncOperation[16];
        _position = new Vector2(transform.position.x, transform.position.z);
        HideShowWorldScenes();
        HideShowInteractiveScenes();
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
        
    }
    private void OnDisable()
    {
        
    }
    private void PlayIntroScene(bool _triggered)
    {
        _introScenePlaying = _triggered;
    }
    private void PlayingIntroScene()
    {
        if(_introScenePlaying && !_ambientaudiosources[0].isPlaying)
        {
            Debug.Log("playing intro from if");
            foreach (AudioSource _source in _ambientaudiosources)
            {
                _source.Stop();
            }
            _ambientaudiosources[0].Play();
        }
        else if(
            _ambientaudiosources[0].isPlaying && !_introScenePlaying) 
        {
            _ambientaudiosources[0].Stop();
        }

    }
    private void Start()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled=true;
    }
    private void FixedUpdate()
    {
        _position = new Vector2(transform.position.x, transform.position.z);
        HideShowWorldScenes();
        HideShowInteractiveScenes();
        //PlayingIntroScene();
    }

    private void HideShowInteractiveScenes()
    {
        //MC Home Town
        if (SceneManager.GetSceneByBuildIndex(4).isLoaded && !_loadingMCTown.IsUnityNull() && _loadingMCTown.isDone && !SceneManager.GetSceneByBuildIndex(6).isLoaded)
        {
            SceneManager.UnloadSceneAsync(4);
            _loadingMCTown = null;
            if (_ambientaudiosources[0].isPlaying)
                _ambientaudiosources[0].Stop();
        }
        else if (!SceneManager.GetSceneByBuildIndex(4).isLoaded && _loadingMCTown.IsUnityNull() && SceneManager.GetSceneByBuildIndex(6).isLoaded)
        {
            _loadingMCTown= SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
            if (!_ambientaudiosources[0].isPlaying)
                _ambientaudiosources[0].Play();
        }

        // Town Square 
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded&&!_loadingTownSquare.IsUnityNull() && !SceneManager.GetSceneByBuildIndex(8).isLoaded && !SceneManager.GetSceneByBuildIndex(9).isLoaded)
        {
            SceneManager.UnloadSceneAsync(2);
            _loadingTownSquare = null;
            SceneManager.UnloadSceneAsync(22);
            _loadingTownSquareNPC = null;
            if (_ambientaudiosources[4].isPlaying)
                _ambientaudiosources[4].Stop();
        }
        else if (!SceneManager.GetSceneByBuildIndex(2).isLoaded && _loadingTownSquare.IsUnityNull()&& SceneManager.GetSceneByBuildIndex(8).isLoaded && SceneManager.GetSceneByBuildIndex(9).isLoaded)
        {
            _loadingTownSquare=SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            _loadingTownSquareNPC = SceneManager.LoadSceneAsync(22, LoadSceneMode.Additive);
            if (!_ambientaudiosources[4].isPlaying)
                _ambientaudiosources[4].Play();
        }
    }
    private void PlayAudios()
    {
        //genral Amibent
        if (!_ambientaudiosources[1].isPlaying) 
            _ambientaudiosources[1].Play();

        //The effects of birds
        if (!_ambientaudiosources[2].isPlaying)
            _ambientaudiosources[2].Play();
    }
    private void HideShowWorldScenes()
    {
        //and also near water add audio source
        //PlayAudios();
        for (int i = 0; i < _worldScenesStartVisible.Length; i++)
        {
            Vector2 _worldStartPos = _worldScenesStartVisible[i];
            _worldStartPos+=new Vector2(_visibleRange,_visibleRange);
            int _si = i + 6;


            //For now all the wolrd has the same audio clip but later we change to farm, woods....
            if (Mathf.Abs(Vector2.Distance(_worldStartPos, _position))/2>_visibleRange+_focalLength)
            {
                if (SceneManager.GetSceneByBuildIndex(_si).isLoaded || (!_worldScenesAO[i].IsUnityNull() && _worldScenesAO[i].isDone))
                {
                    _worldScenesAO[i]= SceneManager.UnloadSceneAsync(_si);
                }
            }
            else
            {

                if (!SceneManager.GetSceneByBuildIndex(_si).isLoaded && (_worldScenesAO[i].IsUnityNull() || _worldScenesAO[i].isDone))
                {

                    _worldScenesAO[i]= SceneManager.LoadSceneAsync(_si, LoadSceneMode.Additive);
                }

            }
            
        }
    }
}
