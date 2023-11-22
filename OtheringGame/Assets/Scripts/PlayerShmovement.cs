using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShmovement : MonoBehaviour
{
    [Header("Movment")]
    [SerializeField] private float speed;
    private Rigidbody2D _rigidbody;

    void Start()
    {
      _rigidbody = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        var playerInput =
            new Vector2(x: Input.GetAxis("Horizontal"), y: Input.GetAxis("Vertical"));

        var playerInputNormalized = playerInput.normalized;

        _rigidbody.velocity = playerInputNormalized * speed;
    }
}
