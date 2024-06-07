using System;
using UnityEngine;
using FMOD.Studio;

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

    [Header("Ground/Fall Settings")]
    [SerializeField] float maxFallSpeed = 40;
    [SerializeField] float fallSpeedAccel = 35;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform feet;
    [SerializeField] Vector3 feetSize;
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
    bool inputRealased = true;
    bool colGrabObj = false;

    Quaternion currentGrabInitialRot;

    bool parentCol;
    bool childCol;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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
        if (InputsBrain.Instance.pause.WasPressedThisFrame())
            GameManager.Instance.PauseGame();

        if (GameManager.Instance.gameIsPause) return;

        if (InputsBrain.Instance.pocket.IsPressed() && hasAstralPocket && inputRealased)
            inputTimer += Time.deltaTime;


        if (InputsBrain.Instance.pocket.WasReleasedThisFrame() && hasAstralPocket && inputRealased)
        {
            if (inputTimer < AstralPocket.Instance.timeToReset)
            {
                if(currentGrabObject != null)
                    UnGrabObject();
                AstralPocket.Instance.CastAstralPocket();
                inputTimer = 0;
            }
            else if (inputTimer > AstralPocket.Instance.timeToReset)
            {
                if (currentGrabObject != null)
                    UnGrabObject();
                AstralPocket.Instance.DecastAstralPocket();
                inputTimer = 0;
            }
        }

        if (InputsBrain.Instance.pocket.WasReleasedThisFrame()) inputRealased = true;

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

        if(currentGrabObject != null)
        {
            if(!IsGrounded() && !colGrabObj)
            {
                colGrabObj = true;
                DeactivateCols(true);
            }
            else if(IsGrounded() && colGrabObj)
            {
                colGrabObj = false;
                DeactivateCols(false);
            }

            if(move.magnitude > .01f && InputsBrain.Instance.rotateGrab.IsPressed())
            {
                currentGrabObject.transform.rotation = currentGrabInitialRot;
                var aimVector = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Lerp(transform.rotation, aimVector, rotateTime * Time.deltaTime);
            }
        }

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

        if(inputTimer > AstralPocket.Instance.timeToReset)
        {
            if (currentGrabObject != null)
                UnGrabObject();
            AstralPocket.Instance.DecastAstralPocket();
            inputTimer = 0;
            inputRealased = false;
        }

    }

    void HUDUpdate()
    {
        if (HUD.Instance == null) return;

        if(CanGrabObject())
            HUD.Instance.grabObj.SetActive(true);
        else if(HUD.Instance.grabObj.activeSelf)
            HUD.Instance.grabObj.SetActive(false);

        if (currentGrabObject != null)
            HUD.Instance.grabRotateObj.SetActive(true);
        else if(HUD.Instance.grabRotateObj.activeSelf)
            HUD.Instance.grabRotateObj.SetActive(false);

        if (DisplayInputs() && hasAstralPocket)
            HUD.Instance.astralInputs.SetActive(true);
        else if(HUD.Instance.astralInputs.activeSelf)
            HUD.Instance.astralInputs.SetActive(false);
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

        if (InputsBrain.Instance.rotateGrab.IsPressed())
            force = Vector3.zero;

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
        currentGrabInitialRot = currentGrabObject.transform.rotation;

        if (currentGrabObject.GetComponent<InteractibleMesh>())
            currentGrabObject = currentGrabObject.transform.parent.gameObject;

        Destroy(currentGrabObject.GetComponent<Rigidbody>());
        currentGrabObject.transform.parent = transform;
        currentGrabObject.transform.localPosition += new Vector3(0, 1f, 0);

        currentGrabObject.TryGetComponent(out Interactible i);
        i.isGrabed = true;
        i.localPosInit = i.transform.localPosition;

        HandleCols();
    }

    void UnGrabObject()
    {
        colGrabObj = false;
        DeactivateCols(false);

        Rigidbody r = currentGrabObject.AddComponent<Rigidbody>();
        r.mass = 1;
        r.freezeRotation = true;
        //r.drag = 2;

        currentGrabObject.TryGetComponent(out Interactible i);
        i.placePos = i.transform.position;
        i.isGrabed = false;
        i._rb = r;
        i.AttachToParent();

        currentGrabObject.transform.rotation = currentGrabInitialRot;
        currentGrabObject = null;
    }

    void HandleCols()
    {
        if(currentGrabObject.TryGetComponent(out Interactible col))
        {
            parentCol = col.col.enabled;
            childCol = col.astraldObj.GetComponent<Collider>().enabled;
        }
    }

    void DeactivateCols(bool yes)
    {
        if (yes)
        {
            if(parentCol)
                currentGrabObject.GetComponent<Collider>().enabled = false;
            if(childCol)
                currentGrabObject.GetComponentInChildren<Interactible>().astraldObj.GetComponent<Collider>().enabled = false;
        }
        else
        {
            if (parentCol)
                currentGrabObject.GetComponent<Collider>().enabled = true;
            if (childCol)
                currentGrabObject.GetComponentInChildren<Interactible>().astraldObj.GetComponent<Collider>().enabled = true;
        }
    }

    #region Boolean
    bool IsGrounded()
    {
        float angle = 0;
        if (slopeHit.normal != Vector3.up)
            angle = Vector3.Angle(Vector3.up, slopeHit.normal);

        return Physics.CheckBox(feet.position, feetSize, Quaternion.identity, ground) && angle < 46;
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

    bool DisplayInputs()
    {
        return Physics.OverlapSphere(transform.position, AstralPocket.Instance.sphereRadius, AstralPocket.Instance.interactibleMask).Length > 0;
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
        Gizmos.DrawCube(feet.localPosition, feetSize);
    }
}
