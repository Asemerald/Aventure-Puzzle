using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    Vector2 moveInputs;
    private Vector3 move;
    
    public static PlayerController Instance;
    private PlayerAnimator _playerAnimator;
    

    [Header("Move Settings")]
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float accel = 1f;
    [SerializeField] private float decel = 1f;
    [SerializeField] private float rotateTime;

    [Header("Jump Settings")]
    [SerializeField] float maxFallSpeed = 40;
    [SerializeField] float fallSpeedAccel = 35;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform feet;
    private float fallSpeed;

    private RaycastHit slopeHit;
    private Vector3 slopeMove;

    [Header("Cam Settings")]
    [SerializeField] private Camera cam;
    [SerializeField] Vector3 camForward, camRight;

    [Header("Grab Settings")]
    [SerializeField] Transform interactCenterPoint;
    [SerializeField] Vector3 grabBoxSize;
    [SerializeField] LayerMask collidingGrabLayers;

    GameObject currentGrabObject;
    float inputTimer;

    public bool hasAstralPocket;


    private void Awake()
    {
        if (Instance == null)
            Instance= this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (TryGetComponent(out PlayerAnimator playerAnimator))
            _playerAnimator = playerAnimator;
        else 
            Debug.LogWarning("No PlayerAnimator component found on " + gameObject.name);
        
        if (TryGetComponent(out Rigidbody Rigidbody))
            rb = Rigidbody;
        else 
            Debug.LogError("No Rigidbody component found on " + gameObject.name);
        
        rb.freezeRotation = true;
        CameraOffset();
    }

    void Update()
    {
        MyInputs();
        HUDUpdate();
        
        if (GameManager.Instance.gameIsPause)
            return;

        CheckMethods();
    }

    void MyInputs()
    {
        if(InputsBrain.Instance.pause.WasPressedThisFrame())
            GameManager.Instance.PauseGame();

        if(GameManager.Instance.gameIsPause) return;

        if (InputsBrain.Instance.pocket.IsPressed() && hasAstralPocket)
            inputTimer += Time.deltaTime;

        if (InputsBrain.Instance.pocket.WasReleasedThisFrame() && hasAstralPocket)
        {
            if(inputTimer < AstralPocket.Instance.timeToReset)
            {
                AstralPocket.Instance.CastAstralPocket();
                inputTimer = 0;
            }
            else if (inputTimer > AstralPocket.Instance.timeToReset)
            {
                AstralPocket.Instance.DecastAstralPocket();
                inputTimer = 0;
            }
        }

        moveInputs = InputsBrain.Instance.move.ReadValue<Vector2>();
        move = moveInputs.x * camRight + moveInputs.y * camForward;

        if (InputsBrain.Instance.interact.IsPressed() && CanGrabObject())
            GrabObject();
        else if (InputsBrain.Instance.interact.WasReleasedThisFrame())
            if (currentGrabObject != null)
                UnGrabObject();
        
        
    }

    void CheckMethods()
    {
        slopeMove = Vector3.ProjectOnPlane(move, slopeHit.normal);

        if (!IsGrounded())
            fallSpeed = Mathf.SmoothStep(fallSpeed, maxFallSpeed, fallSpeedAccel * Time.deltaTime);
        else if (IsGrounded())
            fallSpeed = 0;

        if (OnSlope()) rb.useGravity = false;
        else rb.useGravity = true;

        if (move.magnitude > .01f && currentGrabObject == null)
        {
            var aimVector = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, aimVector, rotateTime * Time.deltaTime);
        }

        if (inputTimer > .2f)
        {
            HUD.Instance.astralSlider.gameObject.SetActive(true);
            HUD.Instance.astralSlider.value = inputTimer;
        }
        else
            HUD.Instance.astralSlider.gameObject.SetActive(false);
    }

    void HUDUpdate()
    {
        if (HUD.Instance == null) return;

        if(CanGrabObject())
            HUD.Instance.grabObj.SetActive(true);
        else
            HUD.Instance.grabObj.SetActive(false);
    }
    

    private void FixedUpdate()
    {
        Move();

        if (!IsGrounded())
            rb.velocity += Vector3.down * fallSpeed;
    }

    private void Move()
    {
        Vector3 movement = new Vector3();

        if(OnSlope())
            movement = slopeMove * maxSpeed;
        else
            movement = move * maxSpeed;

        float acceleration = movement.magnitude > .01f ? accel : decel;
        movement = movement - rb.velocity;

        var force = new Vector3();
        if(OnSlope())
            force = new Vector3(movement.x * acceleration, movement.y * acceleration, movement.z * acceleration);
        else
            force = new Vector3(movement.x * acceleration, rb.velocity.y, movement.z * acceleration);

        rb.AddForce(force, ForceMode.Acceleration);
        

        // Calcul la speed et l'envoie a l'animator
        if(_playerAnimator != null)
        {
            float speed = rb.velocity.magnitude / maxSpeed;
            speed = Mathf.Clamp(speed, 0f, 1f);
            _playerAnimator.SetSpeed(speed);
        }
    }

    private void CameraOffset()
    {
        //Calculer l'orientation de la cam�ra pour garder la bonne direction pour aller vers le haut
        camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        camRight = cam.transform.right;
    }

    void GrabObject()
    {
        currentGrabObject = SortObjectToGrab();

        if (currentGrabObject.GetComponent<InteractibleMesh>())
            currentGrabObject = currentGrabObject.transform.parent.gameObject;

        Destroy(currentGrabObject.GetComponent<Rigidbody>());
        currentGrabObject.transform.parent = transform;
    }

    void UnGrabObject()
    {
        currentGrabObject.AddComponent<Rigidbody>().freezeRotation = true;
        currentGrabObject.GetComponent<Rigidbody>().mass = 100;
        currentGrabObject.transform.parent = null;
        currentGrabObject = null;
    }

    #region Boolean
    bool IsGrounded()
    {
        float angle = 0;
        if (slopeHit.normal != Vector3.up)
            angle = Vector3.Angle(Vector3.up, slopeHit.normal);

        return Physics.CheckSphere(feet.position, 0.15f, ground) && angle < 46;
    }

    bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < 46 && angle != 0;
            }
            else
                return false;
        }
        return false;
    }

    GameObject SortObjectToGrab()
    {
        Collider[] hitted = Physics.OverlapBox(interactCenterPoint.position, grabBoxSize, transform.rotation, collidingGrabLayers);
        if (hitted.Length == 1)
        {
            return hitted[0].gameObject;
        }
        else if(hitted.Length > 1)
        {
            float distance = 100;
            int index = 0;
            for (int i = 0; i < hitted.Length; i++)
            {
                float tempDist = Vector3.Distance(hitted[i].transform.position, transform.position);
                if(tempDist < distance)
                {
                    distance = tempDist;
                    index = i;
                }
            }

            return hitted[index].gameObject;
        }
        else    
            return null;
    }

    bool CanGrabObject()
    {
        if (currentGrabObject != null) 
            return false;

        Collider[] hitted = Physics.OverlapBox(interactCenterPoint.position, grabBoxSize, transform.rotation, collidingGrabLayers);
        if(hitted.Length > 0)
            return true;
        else
            return false;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);


        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(interactCenterPoint.localPosition, grabBoxSize);
        Gizmos.DrawWireSphere(feet.localPosition, 0.15f);
    }
}
