using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float runSpeed = 3f;

    private Rigidbody _rigid;
    private Vector3 moveInput;
   
    private bool isRunning; // 달리는 상태

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _rigid.freezeRotation = true;
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(moveX, 0, moveZ).normalized;
        isRunning = Input.GetKey(KeyCode.LeftShift);

    }

    private void Move()
    {
        float speed;

        if (isRunning)
        {
            speed = runSpeed;
        }
        else
        {
            speed = moveSpeed;
        }

        Vector3 move = transform.TransformDirection(moveInput) * speed;
        _rigid.MovePosition(_rigid.position + move * Time.fixedDeltaTime);
    }
}
