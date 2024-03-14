using Player;
using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        
        
        private Inputs _inputs;
        
        [Header("Settings")] [SerializeField] private Transform _cam;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Animator _anim;
        [SerializeField] private GatherInputs _inputsActions;
        
    
        private void Awake()
        {
            if (_inputsActions == null || _rb == null || _anim == null)
            {
                _inputsActions = GetComponent<GatherInputs>();
                _rb = GetComponent<Rigidbody>();
                _anim = GetComponent<Animator>();
            }
            
        }
        
        private void Update()
        {
            GatherInputs();
            
            HandleGround();
            
            Move();
            
            
        }
        
        private void GatherInputs()
        {
            _inputs.X = _inputsActions.move.x;
            _inputs.Z = _inputsActions.move.y;
            
            _dir = new Vector3(_inputs.X, 0, _inputs.Z);
            
            // Set look direction only if dir is not zero, to avoid snapping back to original and adjust with camera
            var CameraForward = _cam.forward;
            CameraForward.y = 0;
            CameraForward.Normalize();
            
            var CameraRight = _cam.right;
            CameraRight.y = 0;
            CameraRight.Normalize();
            _dir = CameraForward * _inputs.Z + CameraRight * _inputs.X;
            
            if (_dir != Vector3.zero) _anim.transform.forward = _dir;
            

        }
        
        #region Detection
        
        [Header("Detection")] [SerializeField] private LayerMask _groundLayer;
        
        [SerializeField] private float _grounderOffset = -1, _grounderRadius = 0.2f;
        [SerializeField] private float _wallCheckOffset = 0.5f, _wallCheckRadius = 0.38f;
        private bool _isAgainstWall, _pushingWall; 
        public bool IsGrounded; // Public bool to check if the player is grounded
        
        public static event Action OnTouchedGround; // Event to be called when the player touches the ground
        
        private readonly Collider[] _ground = new Collider[1]; // Array to store the results of the sphere cast
        private readonly Collider[] _wall = new Collider[1]; // Array to store the results of the sphere cast


        private void HandleGround()
        {
            // Check if the player is grounded and store the collider that was hit
            var grounded = Physics.OverlapSphereNonAlloc(transform.position + new Vector3(0, _grounderOffset), _grounderRadius, _ground, _groundLayer) > 0;
            
            if (!IsGrounded && grounded)
            {
                IsGrounded = true;
                
                //_hasJumped = false;
                //PlayRandomClip(_landClips); TODO
                //_currentMovementLerpSpeed = 100;
                _anim.SetBool("IsGrounded", true);
                OnTouchedGround?.Invoke();
                
            }
            else if (IsGrounded && !grounded)
            {
                IsGrounded = false;
                _anim.SetBool("IsGrounded", false);
                transform.SetParent(null);
            }
            
        }
        
        
        
        
        private Vector3 WallDetectPosition => _anim.transform.position + Vector3.up + _anim.transform.forward * _wallCheckOffset;
        
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;

            // Grounder
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, _grounderOffset), _grounderRadius);

            // Wall
            Gizmos.DrawWireSphere(WallDetectPosition, _wallCheckRadius);
        }
        
        #endregion
        
        #region Movement
        
        [Header("Movement")] [SerializeField] private float _movementSpeed = 5;
        [SerializeField] private float _acceleration = 5;
        [SerializeField] private float _maxWalkingPenalty = 5;
        [SerializeField] private float _currentMovementLerpSpeed = 100;
        private float _currentWalkingPenalty;
        
        private Vector3 _dir;
        
        private void Move() {
            _currentMovementLerpSpeed = Mathf.MoveTowards(_currentMovementLerpSpeed, 100, 20 * Time.deltaTime);

            var normalizedDir = _dir.normalized;

            // Slowly increase max speed
            if (_dir != Vector3.zero) _currentWalkingPenalty += _acceleration * Time.deltaTime;
            else _currentWalkingPenalty -= _acceleration * Time.deltaTime;
            _currentWalkingPenalty = Mathf.Clamp(_currentWalkingPenalty, _maxWalkingPenalty, 1);

            // Set current y vel and add walking penalty
            var targetVel = new Vector3(normalizedDir.x, _rb.velocity.y, normalizedDir.z) * _currentWalkingPenalty * _movementSpeed;

            // Set vel
            var idealVel = new Vector3(targetVel.x, _rb.velocity.y, targetVel.z);
            

            _rb.velocity = Vector3.MoveTowards(_rb.velocity, idealVel, _currentMovementLerpSpeed * Time.deltaTime);

            _anim.SetBool("Walking", _dir != Vector3.zero && IsGrounded);
        }
        
        
        
        
        #endregion
        

    

        #region Audio

        [Header("Audio")] [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip[] _landClips;
        [SerializeField] private AudioClip[] _stepClips;

        private void PlayRandomClip(AudioClip[] clips) {
            _source.PlayOneShot(clips[Random.Range(0, clips.Length)], 0.2f);
        }

        #endregion

        private struct Inputs {
            public float X, Z;
            public int RawX, RawZ;
        }
    }
}
