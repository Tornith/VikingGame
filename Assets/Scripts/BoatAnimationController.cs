using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.AI;

public class BoatAnimationController : MonoBehaviour
{
    public Animator animator;
    public bool isMoving;
    public bool isRotating;
    public bool useAgent;
    
    private Rigidbody _rb;
    private BoatEnemy _agent;
    private static readonly int LeftSpeed = Animator.StringToHash("LeftSpeed");
    private static readonly int RightSpeed = Animator.StringToHash("RightSpeed");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Start()
    {
        if (animator == null) return;
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<BoatEnemy>();
        // Set both layers to active
        animator.SetLayerWeight(0, 1);
        animator.SetLayerWeight(1, 1);
    }
    
    private void Update()
    {
        if (animator == null) return;
        var forwardVelocity = Vector3.Dot(transform.forward, useAgent ? _agent.Velocity * 100 : _rb.velocity);
        var sidewaysVelocity = Vector3.Dot(transform.right, useAgent ? _agent.Velocity * 100 : _rb.velocity);

        if (isRotating && !isMoving)
        {
            animator.SetFloat(LeftSpeed, -sidewaysVelocity);
            animator.SetFloat(RightSpeed, sidewaysVelocity);

            animator.SetBool(IsMoving, true);
        }
        else
        {
            animator.SetFloat(LeftSpeed, forwardVelocity);
            animator.SetFloat(RightSpeed, forwardVelocity);

            animator.SetBool(IsMoving, isMoving);
        }
    }
}
