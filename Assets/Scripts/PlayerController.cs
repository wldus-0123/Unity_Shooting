using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] CharacterController controller;

    [Header("Spec")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;

    private Vector3 moveDir;
    private float ySpeed;

	private void Update()
	{
        Move();
        JumpMove();
	}

    private void Move()
    {
		//controller.Move(moveDir * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
	}

    private void JumpMove()
    {
        // 등가속도 운동 : 속도를 시간에 따라서 점점 가속하는 경우
        ySpeed += Physics.gravity.y * Time.deltaTime;  // y에 중력을 계속 가해줌

        if(controller.isGrounded)
        {
            ySpeed = 0;
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

	private void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveDir.x = inputDir.x;
        moveDir.z = inputDir.y;
    }

    private void OnJump(InputValue value)
    {

		if (controller.isGrounded)  // isGround 구리니까 그냥 groundChecker 구현하는게 나음
		{
			ySpeed = jumpSpeed;
		}
	
    }
}
