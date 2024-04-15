using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    Vector2 moveInputs;
    private Vector3 move;

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

    [Header("Interact Settings")]
    [SerializeField] Transform interactCenterPoint;
    [SerializeField] Vector3 attackBoxSize;
    [SerializeField] LayerMask collidingLayers;
    [SerializeField] ParticleSystem slashVFX;

    [Header("Grab Settings")]
    [SerializeField] Vector3 grabBoxSize;
    [SerializeField] LayerMask collidingGrabLayers;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [SerializeField] Rigidbody currentGrabbedObject;
    [SerializeField] MoveableObject closestMoveableObject;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        currentHealth = maxHealth;
        CameraOffset();
    }

    void Update()
    {
        MyInputs();
        HUDUpdate();
        
        if (GameManager.Instance.inTarotInventory || GameManager.Instance.gameIsPause)
            return;

        CheckMethods();
    }

    void MyInputs()
    {
        if(InputsBrain.Instance.pause.WasPressedThisFrame())
            GameManager.Instance.PauseGame();

        if(GameManager.Instance.gameIsPause) return;

        if (InputsBrain.Instance.tarot.WasPressedThisFrame())
            GameManager.Instance.CheckTarotInventory();

        if (GameManager.Instance.inTarotInventory)
        {
            if (InputsBrain.Instance.interract.WasPressedThisFrame())
                TarotInventory.Instance.SwitchCardState();
        }
        else
        {
            moveInputs = InputsBrain.Instance.move.ReadValue<Vector2>();
            move = moveInputs.x * camRight + moveInputs.y * camForward;

            if (InputsBrain.Instance.interract.IsPressed() && CanGrabObject())
                GrabObject();
            else if (InputsBrain.Instance.interract.WasReleasedThisFrame())
                if (currentGrabbedObject != null)
                    UnGrabObject();
        }
        
    }

    void CheckMethods()
    {
        if (!IsGrounded())
            fallSpeed = Mathf.SmoothStep(fallSpeed, maxFallSpeed, fallSpeedAccel * Time.deltaTime);
        else
            fallSpeed = 0;

        if (move.magnitude > .01f && currentGrabbedObject == null)
        {
            var aimVector = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, aimVector, rotateTime * Time.deltaTime);
        }

        if (CanGrabObject() && currentGrabbedObject == null)
        {
            Collider[] hitted = Physics.OverlapBox(interactCenterPoint.position, grabBoxSize, transform.rotation, collidingGrabLayers);

            if (hitted.Length > 0)
            {
                float closest = 10;
                int index = 0;

                for (int i = 0; i < hitted.Length; i++)
                    if (Vector3.Distance(hitted[i].transform.position, transform.position) < closest)
                    {
                        index = i;
                        closest = Vector3.Distance(hitted[i].transform.position, transform.position);
                    }

                closestMoveableObject = hitted[index].GetComponent<MoveableObject>();
            }
        }
        else if (!CanGrabObject() && currentGrabbedObject != null)
            UnGrabObject();
        else
            closestMoveableObject = null;
    }

    void UnGrabObject()
    {
        currentGrabbedObject.velocity = Vector3.zero;
        currentGrabbedObject.mass = 100;
        currentGrabbedObject = null;
    }

    void GrabObject()
    {
        if(currentGrabbedObject == null && closestMoveableObject != null)
        {
            currentGrabbedObject = closestMoveableObject.GetComponent<Rigidbody>();
            currentGrabbedObject.mass = 1;
        }
    }

    void HUDUpdate()
    {
        if (HUD.Instance == null) return;

        if(CanGrabObject() && currentGrabbedObject == null && !GameManager.Instance.inTarotInventory)
            HUD.Instance.grabObj.SetActive(true);
        else
            HUD.Instance.grabObj.SetActive(false);
    }

    /*void ActivateInteracable()
    {
        Collider[] hitted = Physics.OverlapBox(interactCenterPoint.position, grabBoxSize, transform.rotation, collidingGrabLayers);
        if (hitted.Length > 0)
        {
            float closest = 10;
            int index = 0;
            for (int i = 0; i < hitted.Length; i++)
            {
                if (Vector3.Distance(hitted[i].transform.position, transform.position) < closest)
                {
                    index = i;
                    closest = Vector3.Distance(hitted[i].transform.position, transform.position);
                }
            }
            //hitted[index].GetComponent<>().Function();
        }
    }*/

    private void FixedUpdate()
    {
        Move();

        if (!IsGrounded())
            rb.velocity += Vector3.down * fallSpeed;

        if (currentGrabbedObject != null)
            currentGrabbedObject.velocity = rb.velocity;

    }

    private void Move()
    {
        // calculate move vector on slopes
        slopeMove = Vector3.ProjectOnPlane(move, slopeHit.normal);

        Vector3 movement = new Vector3();

        if(!OnSlope())
            movement = move * maxSpeed;
        else
            movement = slopeMove * maxSpeed;

        float acceleration = movement.magnitude > .01f ? accel : decel;
        movement = movement - rb.velocity;
        var force = new Vector3(movement.x * acceleration, rb.velocity.y, movement.z * acceleration);

        rb.AddForce(force, ForceMode.Acceleration);
    }

    private void CameraOffset()
    {
        //Calculer l'orientation de la cam�ra pour garder la bonne direction pour aller vers le haut
        camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        camRight = cam.transform.right;
    }

    public void TakeDamage()
    {
        Debug.Log("damage");
        if (currentHealth == 0)
            return;

        currentHealth -= 1;
    }

    #region Boolean
    bool IsGrounded()
    {
        return Physics.CheckSphere(feet.position, 0.3f, ground);
    }

    bool OnSlope()
    {
        if (Physics.Raycast(feet.position, Vector3.down, out slopeHit, 0.3f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < 45 && angle != 0;
            }
            else
                return false;
        }
        return false;
    }

    bool CanGrabObject()
    {
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
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(interactCenterPoint.localPosition, attackBoxSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(interactCenterPoint.localPosition, grabBoxSize);
    }
}
