using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float runSpeed = 5f;

    private Rigidbody _rigid;
    private Vector3 moveInput;
   
    private bool isRunning; // 달리는 상태

    private Animator _animator;
    private PlayerFootstepHandler _footstepHandler;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _rigid.freezeRotation = true;

        _animator = GetComponent<Animator>();
        _footstepHandler = GetComponent<PlayerFootstepHandler>();
    }

    private void Update()
    {
        PlayerInput();
        MoveAnimator();
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

        if (_footstepHandler != null)
        {
            _footstepHandler.isRunning = isRunning;
        }
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

    private void MoveAnimator()
    {
        bool isMoving = moveInput.magnitude > 0f;

        _animator.SetBool("IsWalk", isMoving && !isRunning);
        _animator.SetBool("IsRun", isMoving && isRunning);
    }

}
