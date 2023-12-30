using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShmovement : MonoBehaviour
{
    [Header("Movment")]
    [SerializeField] private float speed;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;

    public Animator anim;
    private bool isWalking;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isWalking = false;
    }

   
    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        Move();

    }
    private void Move()
    {
        Vector2 moveDirection = InputManager.GetInstance().GetMoveDirection();
        _rigidbody.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);

        
        if(moveDirection != Vector2.zero)
        {
            if(!isWalking)
            {
                isWalking = true;
                anim.SetBool("IsMoving", isWalking);
            }
        }
        
        else
        {
            if(isWalking)
            {
                isWalking = false;
                anim.SetBool("IsMoving", isWalking);
            }
        }
    }
}
