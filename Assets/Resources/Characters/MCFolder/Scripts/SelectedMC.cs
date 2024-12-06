
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class SelectedMC : MCComponent
{

    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 5.0f;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 8f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    public AudioClip[] GetHitAudioClips; //0 female, 1 male
    public AudioClip LandingAudioClip;
    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -8.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;

    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = 0.2f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.2f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 20.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -10.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;// cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    // player
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private Vector3 _targetDirection=Vector3.zero;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    private float _specialAttackCoolOffTime = 15f;
    private int _simpleAttackTapCount = 0;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    private int _animIDSimpleAttack;
    private int _animIDSpecialAttack;
    private int _animIDGetHit;
    private Animator _animator;
    private NavMeshAgent _agent;
    private GameObject _mainCamera;

    private const float _threshold = 0.01f;

    private bool _hasAnimator;
    /// <summary>
    /// ////////////////
    /// </summary>
    public static SelectedMC Instance { get; set; }
    private bool sprint;
    private bool jump;
    private Vector2 _look=Vector2.zero;
    private Vector2 _moveVector = Vector2.zero;
    private bool _simpleAttackTriggered;
    private bool _specialAttackTriggered;
    private bool _getHit;


    //this to load player in current saved position and use it ro run/move ...
    private Vector3 Position { get; set; }
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
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

        _hasAnimator = gameObject.TryGetComponent(out _animator);
        _agent=gameObject.GetComponent<NavMeshAgent>(); 

        AssignAnimationIDs();


        //Rigidbody _rb = gameObject.GetComponent<Rigidbody>();
        _agent.updateRotation = true;
        _agent.updatePosition = true;
        // reset our timeouts on start
        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }
    private void OnEnable()
    {
        SimpleAttackComponent.OnSimpleAttackTriggered += ApplySimpleAttack;
        SpecialAttackComponent.OnSpecialAttackTriggered += ApplySpecialAttack;

        MovementComponent.OnMoveInput += ApplyMove;
        RunComponent.OnRunTriggered += ApplyRun;
        JumpComponent.OnJumpTriggered += ApplyJump;
        LookComponent.OnLookInput += CameraRotation;
    
    }
    private void OnDisable()
    {
        SimpleAttackComponent.OnSimpleAttackTriggered -= ApplySimpleAttack;
        SpecialAttackComponent.OnSpecialAttackTriggered -= ApplySpecialAttack;

        MovementComponent.OnMoveInput -= ApplyMove;
        RunComponent.OnRunTriggered -= ApplyRun;
        JumpComponent.OnJumpTriggered -= ApplyJump;
        LookComponent.OnLookInput -= CameraRotation;

    }
    private void FixedUpdate()
    {
        _hasAnimator = TryGetComponent(out _animator);

        JumpAndGravity();
        GroundedCheck();
        Move();

        SimpleAttack();
        SpecialAttack();
        ReceiveDamage();

      
    }
    private void LateUpdate()
    {
        RotateCamera();
    }
    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDSimpleAttack = Animator.StringToHash("SimpleAttack");
        _animIDSpecialAttack = Animator.StringToHash("SpecialAttack");
        _animIDGetHit = Animator.StringToHash("GetHit");

        _animator.SetBool(_animIDGetHit, false);
        //Simple attack animation tripples
    }
    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        //Debug.Log("SpherePosition:    " + spherePosition);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
      

        // update animator if using character
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDGrounded, Grounded);

        }
    }
    private void RotateCamera()
    {
        if (_look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            Debug.Log("look magnitude:    " + _look.magnitude);
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = Time.deltaTime; //

            _cinemachineTargetYaw += _look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _look.y * deltaTimeMultiplier;
            // clamp our rotations
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
            //Rotate Player
            _targetRotation = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

            // rotate to face input direction relative to camera position
            //gameObject.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            _look = Vector2.zero;
        }

       
    }
    private void CameraRotation(Vector2 look)
    {
        _look = look;
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    
    private void SimpleAttack()
    {
        if (_simpleAttackTriggered && _hasAnimator)
        {
            _animator.SetBool(_animIDSimpleAttack, true);
            _simpleAttackTriggered = false;
            StartCoroutine(WaitEndOfAnimationSimpleAttack());
        }
        /* if (OnSimpleAttackDaggerTriggered != null)
         {
             _tapCount++;
             OnSimpleAttackDaggerTriggered(true);
             if (_tapCount >= 3)
             {
                 OnSimpleAttackDynamicTriggered(true);
                 _tapCount = 0;
             }
         }*/
        //if Dynamic is triggered --> interrupt Dagger aniamtion + damage and do this
        //Reset queue of actions
        //Apply damage to oponent 
        //Play animation <-- shoudl eachanimation contain audio ? 
    }

    public void SpecialAttack()
    {
        //Debug.Log("special attack griggered");
    }
    public void ApplySimpleAttack(bool _trigered)
    {
        _simpleAttackTriggered = _trigered;
    }
    public void ApplySpecialAttack(bool _trigered)
    {
        _specialAttackTriggered = _trigered;
        //Add function to decrease the cool off time that blocks the 
        //special attack button and releases it after cool off 
        //may use animation here for overlay and blocking raycast
    }
    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = sprint ? SprintSpeed : MoveSpeed;


        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        float inputMagnitude = 1f;
        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0

        if (_moveVector == Vector2.zero)
        {
            targetSpeed = 0.0f;
            sprint = false;

        }

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_agent.velocity.x, 0.0f, _agent.velocity.z).magnitude;

        float speedOffset = 0.1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(_moveVector.x, 0.0f, _moveVector.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_moveVector != Vector2.zero)
        {
            /* _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                               _mainCamera.transform.eulerAngles.y;*/
            Debug.Log("Move Vector:   " + _moveVector);

            float _rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);
            Debug.Log("Rotation:    " + _rotation);
            Debug.Log("Target Rotation:    " + _targetRotation);
            _targetDirection = Quaternion.Euler(0.0f, _rotation, 0.0f) * Vector3.forward;

            _agent.Move(_targetDirection.normalized * (_speed * Time.deltaTime) +
                          new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

       

        if (_moveVector == Vector2.zero)
        {
            _animationBlend = 0f;

        }
        // update animator if using character
        if (_hasAnimator)
        {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);

        }
    }
    public void ApplyMove(Vector2 moveVector)
    {
        _moveVector = moveVector;
        
    }


    public void ApplyRun(bool _triggered)
    {
        Debug.Log("Triggered Run");
        sprint = _triggered;
        if(!sprint)
            _speed = 0.0f;
    }
    private void JumpAndGravity()
    {
        
        if (Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDFreeFall, false);
            }

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (jump && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight *-2f* Gravity);

                Debug.Log(_verticalVelocity);
                //gameObject.transform.position.Set(gameObject.transform.position.x, _verticalVelocity, gameObject.transform.position.z);
                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, true);
                }
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDFreeFall, true);
                    
                    //_animator.fireEvents = true;
                }

            }

            // if we are not grounded, do not jump
            jump = false;
        }
        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }
    public void ApplyJump(bool _trigered)
    {
        jump = _trigered;
        
    }
    public void GetHit(bool triggered)
    {
        _getHit = triggered;
    }
    private void ReceiveDamage()
    {
        
        if(_hasAnimator && _getHit)
        {
            _animator.SetBool(_animIDGetHit, true);
            _getHit = false;
            StartCoroutine(WaitEndOfAnimationGetHit());
        }
    }
    private IEnumerator WaitEndOfAnimationGetHit()
    {
        yield return new WaitForSecondsRealtime(2f);
        _getHit = false;
        _animator.SetBool(_animIDGetHit, false);
    }
    private IEnumerator WaitEndOfAnimationSimpleAttack()
    {
        yield return new WaitForSecondsRealtime(2f);
        _animator.SetBool(_animIDSimpleAttack, false);
    }
    public void SendDamage()
    {
        //this function will be like an event 
        //will be called inside attack 
        //if enemy in vicinity --> enemy<-- receiveDamage
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, FootstepAudioVolume);
            }
        }
    }
    private void OnGetHit(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(GetHitAudioClips[MCSelectionComponent.Instance.GetGender()], transform.position, FootstepAudioVolume);
        }
   }
    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(LandingAudioClip, transform.position, FootstepAudioVolume);
        }
    }
    public void MCKilled()
    {
        Debug.Log("joystick touched");
        //If MC is killed --> LifeSpan =0
        //If he is doing a current task --> the progress of current task will be reset to 0
        //the MC will span again with full _lifespan 
        //any remaining enemies will be killed but respayned when task is starting again
    }

}
