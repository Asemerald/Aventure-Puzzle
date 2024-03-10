using Player;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Animator _anim;
        private GatherInputs _inputsActions;
        
    
        private void Awake()
        {
            _inputsActions = GetComponent<GatherInputs>();
        }
        
        private void Update()
        {
            GatherInputs();
        }
        
        private void GatherInputs()
        {
            
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
                OnTouchedGround?.Invoke();
            }
            else if (IsGrounded && !grounded)
            {
                IsGrounded = false;
                _anim.SetBool("IsGrounded", false);
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
        
        
    }
}
