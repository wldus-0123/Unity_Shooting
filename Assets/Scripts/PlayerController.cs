using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;

    [Header("Spec")]
    [SerializeField] float moveSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpSpeed;  // ���� �������� ��

    private Vector3 moveDir;
    private float ySpeed; // �Ʒ��� �������� ��
    private bool isWalk;  // �ȴ� ������ �ƴ��� �Ǵ�

	private void Update()
	{
        Move();
        JumpMove();
	}

    private void Move()
    {
        if (isWalk)
        {
			controller.Move(transform.right * moveDir.x * walkSpeed * Time.deltaTime);
			controller.Move(transform.forward * moveDir.z * walkSpeed * Time.deltaTime);
            animator.SetFloat("XSpeed", moveDir.x * walkSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("YSpeed", moveDir.z * walkSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("MoveSpeed", moveDir.magnitude * walkSpeed, 0.1f, Time.deltaTime);
		}

        else
        {
			controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
			controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
			animator.SetFloat("XSpeed", moveDir.x * moveSpeed, 0.1f, Time.deltaTime);
			animator.SetFloat("YSpeed", moveDir.z * moveSpeed, 0.1f, Time.deltaTime);
			animator.SetFloat("MoveSpeed", moveDir.magnitude * moveSpeed, 0.1f, Time.deltaTime);
		}
		//controller.Move(moveDir * moveSpeed * Time.deltaTime);
        
        
	}

    private void JumpMove()
    {
        // ��ӵ� � : �ӵ��� �ð��� ���� ���� �����ϴ� ���
        ySpeed += Physics.gravity.y * Time.deltaTime;  // y�� �߷��� ��� ������ (�Ʒ��� �������� ��)

        if(controller.isGrounded)  // ���� ���� ���� �Ʒ��� ���ϴ� ���� 0���� �������� (�����ؾߵǴϱ�)
        {
            ySpeed = 0;
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void Fire()
    {
        animator.SetTrigger("Fire");
    }

    private void Reload()
    {
        animator.SetTrigger("Reload");
    }

	private void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveDir.x = inputDir.x;
        moveDir.z = inputDir.y;
    }

    private void OnJump(InputValue value)
    {
		ySpeed = jumpSpeed;

		//if (controller.isGrounded)  // isGround �����ϱ� �׳� groundChecker �����ϴ°� ����
		//{
		//	ySpeed = jumpSpeed;
		//}

	}

    private void OnWalk(InputValue value)
    {
        if(value.isPressed)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
    }

    private void OnFire(InputValue Value) 
    {
        Fire();
    }

	private void OnReload(InputValue Value)
	{
		Reload();
	}
}
