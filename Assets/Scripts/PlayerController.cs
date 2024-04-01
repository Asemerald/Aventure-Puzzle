using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Attack Settings")]
    [SerializeField] Transform attackCenterPoint;
    [SerializeField] Vector3 attackBoxSize;
    [SerializeField] LayerMask collidingLayers;
    [SerializeField] ParticleSystem slashVFX;

    [Header("Activable Settings")]
    [SerializeField] Vector3 activableBoxSize;
    [SerializeField] LayerMask collidingActivableLayers;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

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
        
        if (GameManager.Instance.inTarotInventory)
            return;

        CheckMethods();
        HUDUpdate();
    }

    void MyInputs()
    {
        if (InputsBrain.Instance.tarot.WasPressedThisFrame())
            GameManager.Instance.CheckTarotInventory();

        if (GameManager.Instance.inTarotInventory)
        {
            if (InputsBrain.Instance.interract.WasPressedThisFrame())
            {
                TarotInventory.Instance.SwitchCardState();
            }
        }
        else
        {
            moveInputs = InputsBrain.Instance.move.ReadValue<Vector2>();
            move = moveInputs.x * camRight + moveInputs.y * camForward;

            if (InputsBrain.Instance.interract.WasPressedThisFrame())
            {
                //Fonction attacker / interagir
                Attack();
            }
        }

        
    }

    void CheckMethods()
    {
        if (!IsGrounded())
            fallSpeed = Mathf.SmoothStep(fallSpeed, maxFallSpeed, fallSpeedAccel * Time.deltaTime);
        else
            fallSpeed = 0;

        if (move.magnitude > .1f)
        {
            var aimVector = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, aimVector, rotateTime * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (!IsGrounded())
            rb.velocity += Vector3.down * fallSpeed;

        Move();
    }

    void HUDUpdate()
    {
        /*if (HUD.Instance == null) return;

        HUD.Instance.shootSlider.value = shootTimer;

        //HUD to display use a lever or something
        Collider[] hitted = Physics.OverlapBox(attackCenterPoint.position, activableBoxSize, transform.rotation, collidingActivableLayers);
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
            if (!hitted[index].GetComponent<LeverScript>().leverHasBeenUsed)
                HUD.Instance.interactText.text = "Press 'Y' to activate lever";
            else
                HUD.Instance.interactText.text = "";
        }
        else
            HUD.Instance.interactText.text = "";*/
    }

    void ActivateInteracable()
    {
        Collider[] hitted = Physics.OverlapBox(attackCenterPoint.position, activableBoxSize, transform.rotation, collidingActivableLayers);
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
            //hitted[index].GetComponent<LeverScript>().ActivateLever();
        }
    }

    private void Move()
    {
        // calculate move vector on slopes
        slopeMove = Vector3.ProjectOnPlane(move, slopeHit.normal);

        // apply forces
        if (!OnSlope())
            rb.velocity += (move * maxSpeed - rb.velocity) * (Time.deltaTime * (move.magnitude > 0.1f ? accel : decel));
        else
            rb.velocity += (slopeMove * maxSpeed - rb.velocity) * (Time.deltaTime * (slopeMove.magnitude > 0.1f ? accel : decel));
    }

    private void CameraOffset()
    {
        //Calculer l'orientation de la cam�ra pour garder la bonne direction pour aller vers le haut
        camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        camRight = cam.transform.right;
    }


    void Attack()
    {
        //slashVFX.Play();
        Collider[] hitted = Physics.OverlapBox(attackCenterPoint.position, attackBoxSize, transform.rotation, collidingLayers);
        if (hitted.Length > 0)
        {
            foreach(Collider c in hitted)
            {
                if (c.GetComponent<Feedbacks>())
                    c.GetComponent<Feedbacks>().Feedback();
            }
        }
    }

    public void TakeDamage()
    {
        Debug.Log("damage");
        if (currentHealth == 0)
            return;

        currentHealth -= 1;
    }

    #region Boolean
    private bool IsGrounded()
    {
        return Physics.CheckSphere(feet.position, 0.3f, ground);
    }

    private bool OnSlope()
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

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(attackCenterPoint.localPosition, attackBoxSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(attackCenterPoint.localPosition, activableBoxSize);
    }
}
