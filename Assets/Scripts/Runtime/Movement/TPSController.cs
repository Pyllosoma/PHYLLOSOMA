using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MovementAssets
{
    public enum ControlSchemas { KeyboardMouse, JoyStick };

    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class TPSController : MonoBehaviour
    {
        [Header("Player Settings")]
        //Move Speed------------------------------------------------------
        [Tooltip("걷기 속도")]
        public float moveSpeed = 2.0f;

        [Tooltip("달리기 속도")]
        public float sprintSpeed = 5.0f;

        [Tooltip("가속 and 감속 가중치")]
        public float SpeedChangeRate = 10.0f;

        private float _speed;

        //Audio Clips------------------------------------------------------
        [Tooltip("착지 오디오클립")]
        public AudioClip landingAudioClip;

        [Tooltip("걷기 오디오클립")]
        public AudioClip [] footStepAudioClips;

        [Tooltip("걷기,착지 오디오 볼륨")]
        [Range(0, 1)] public float footStepAudioVolume = 0.5f;

        //Jump------------------------------------------------------
        [Tooltip("점프 높이")]
        public float jumpHeight = 1.2f;

        [Tooltip("점프 쿨타임")]
        public float jumpTimeout = 0.5f;

        [Tooltip("점프 쿨타임 타이머")]
        public float jumpTimeoutDelta;

        [Tooltip("중력")]
        public float gravity = -15.0f;

        public float _verticalvelocity; //현재 점프 속도

        private float _terminalVelocity = 53.0f; //최대 점프 속도 제한

        //Grounded------------------------------------------------------
        [Tooltip("지면 착지 여부")]
        public bool grounded = true;

        [Tooltip("지면 착지 여부를 검사하는 Ray구체의 y축 위치 가중치")]
        public float groundedOffset = -0.14f;

        [Tooltip("지면 착지 여부를 검사하는 Ray구체 반지름")]
        public float groundedRadius = 0.28f;

        [Tooltip("지면 레이어")]
        public LayerMask groundLayers;

        //Camera------------------------------------------------------
        [Tooltip("시네머신 가상카메라가 따라다닐 오브젝트")]
        public GameObject cinemachineCameraTarget;

        [Tooltip("카메라 상하회전 각도 제한(위)")]
        public float topClamp = 70f;

        [Tooltip("카메라 상하회전 각도 제한(아래)")]
        public float botomClamp = -30f;

        [Tooltip("카메라 회전 잠금")]
        public bool lockCameraPosition = false;

        [Tooltip("카메라 좌우 회전 합산 값 저장")]
        public float cinemachineTargetYaw;

        [Tooltip("카메라 상하 회전 합산 값 저장")]
        public float cinemachineTargetPitch;

        [Tooltip("마우스 좌우 회전 민감도")]
        [Range(0.1f, 5f)] public float mouseHorizontalSensitivity = 1f;

        [Tooltip("마우스 상하 회전 민감도")]
        [Range(0.1f, 5f)] public float mouseVerticalSensitivity = 1f;

        [Tooltip("마우스 상하 회전 반전")]
        public bool mouseVerticalInvert = false;

        //Animatior Parameters IDs------------------------------------------------------
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDFreeFall;
        private int _animIDJump;
        private int _animIDMotionSpeed;
        private int _animaKneel;

        //Components------------------------------------------------------
        private Animator _animator;
        private PlayerInput _playerInput;
        private CharacterController _characterController;
        private MovementAssetsInputs _inputs;
        public GameObject mainCamera;

        //Else------------------------------------------------------
        private const float _threshold = 0.1f; //For Camera Rotation
        private bool _hasAniamtor; //Animator Working Check
        public bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == ControlSchemas.KeyboardMouse.ToString();
#else
                return false;
#endif
            }
        }//Is Current Schema PC
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;



        // Start is called before the first frame update
        void Awake()
        {
            //Check MainCamera
            if (mainCamera == null)
            {
                Debug.LogError("MainCamera is null, FindGameObject is not recommended, please mapping maincamera");
                mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

            //Get Components
            _hasAniamtor = TryGetComponent(out _animator);
            _playerInput = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();
            _inputs = GetComponent<MovementAssetsInputs>();
        }

        private void Start()
        {
            //Assign Animation IDs
            AssignAnimationIDs();

            //Reset Jump Timeout delta
            jumpTimeoutDelta = jumpTimeout;

            //Initialize cinemachineTargetYaw
            cinemachineTargetYaw = cinemachineCameraTarget.transform.rotation.eulerAngles.y;
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDJump = Animator.StringToHash("Jump");
            _animaKneel = Animator.StringToHash("Kneel");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        // Update is called once per frame
        void Update()
        {
            _hasAniamtor = TryGetComponent(out _animator);
            GroundedCheck();
            JumpAndGravity();
            Move();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void GroundedCheck() 
        {
            Vector3 checkSphere = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            grounded = Physics.CheckSphere(checkSphere, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

            if (_hasAniamtor)
            {
                _animator.SetBool(_animIDGrounded, grounded);
            }
        }

        private void JumpAndGravity()
        {
            if (grounded)
            {
                if (_hasAniamtor)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                if (_verticalvelocity < 0f)
                {
                    _verticalvelocity = -2f;
                }

                if (_inputs.Jump && jumpTimeoutDelta <= 0f)
                {
                    _verticalvelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

                    if (_hasAniamtor)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                if (jumpTimeoutDelta >= 0.0f)
                {
                    jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                jumpTimeoutDelta = jumpTimeout;

                if (_hasAniamtor)
                {
                    _animator.SetBool(_animIDFreeFall, true);
                }

                _inputs.SetOffJump();
            }

            if (_verticalvelocity < _terminalVelocity)
            {
                _verticalvelocity += gravity * Time.deltaTime;
            }
        }

        private void Move()
        {
            float targetSpeed = _inputs.Sprint ? sprintSpeed : moveSpeed; //실제 속도

            if(_inputs.Move == Vector2.zero) targetSpeed = 0f;

            float currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0f, _characterController.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _inputs.analogMovement ? _inputs.Move.magnitude : 1f;

            if (currentHorizontalSpeed < targetSpeed - speedOffset || 
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate); //애니메이터 파라미터 speed 값
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            Vector3 inputDirection = new Vector3(_inputs.Move.x, 0f, _inputs.Move.y).normalized;
            if (_inputs.Move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;

            _characterController.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0f, _verticalvelocity, 0f) * Time.deltaTime);


            if (_hasAniamtor)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void CameraRotation()
        {
            if (_inputs.Look.sqrMagnitude >= _threshold && !lockCameraPosition)
            {
                //if Control Schema is not PC, use deltaMultiplier for slow down on joystick
                float deltaMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;


                //Check Mouse Vertical Invert
                float targetY = mouseVerticalInvert ? _inputs.Look.y : -(_inputs.Look.y);
                cinemachineTargetPitch += targetY * mouseVerticalSensitivity * deltaMultiplier;
                cinemachineTargetYaw += _inputs.Look.x * mouseHorizontalSensitivity * deltaMultiplier;
            }

            cinemachineTargetPitch = ClampVerticalAngle(cinemachineTargetPitch, botomClamp, topClamp);
            cinemachineTargetYaw = ClampHorizontalAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);

            cinemachineCameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch, cinemachineTargetYaw, 0f);
        }

        private float ClampVerticalAngle(float currentAngle, float minAngle, float maxAngle)
        {
            if (currentAngle > 360f) currentAngle -= 360f;
            if (currentAngle < -360f) currentAngle += 360f;
            return Mathf.Clamp(currentAngle, minAngle, maxAngle);
        }

        private float ClampHorizontalAngle(float currentAngle, float minAngle, float maxAngle)
        {
            if (currentAngle > 360f) currentAngle -= 360f;
            if (currentAngle < -360f) currentAngle += 360f;
            return Mathf.Clamp(currentAngle, minAngle, maxAngle);
        }

        private void OnDrawGizmos()
        {
            Color transparentGreen = new Color(0f, 1f, 0f, 0.35f);
            Color transparentRed = new Color(1f, 0f, 0f, 0.35f);

            if (grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            Vector3 gizmoPosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            Gizmos.DrawSphere(gizmoPosition, groundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (footStepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, footStepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(footStepAudioClips[index], transform.TransformPoint(_characterController.center), footStepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.3f)
            {
                Debug.Log(2);
                AudioSource.PlayClipAtPoint(landingAudioClip, transform.TransformPoint(_characterController.center), footStepAudioVolume);
            }
        }
    }


}

