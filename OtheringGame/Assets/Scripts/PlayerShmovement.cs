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

    void Start()
    {
      _rigidbody = GetComponent<Rigidbody2D>();
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
    }
}
