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
        // ��ӵ� � : �ӵ��� �ð��� ���� ���� �����ϴ� ���
        ySpeed += Physics.gravity.y * Time.deltaTime;  // y�� �߷��� ��� ������

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

		if (controller.isGrounded)  // isGround �����ϱ� �׳� groundChecker �����ϴ°� ����
		{
			ySpeed = jumpSpeed;
		}
	
    }
}
