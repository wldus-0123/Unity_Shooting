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
    [SerializeField] float jumpSpeed;  // 위로 가해지는 힘

    private Vector3 moveDir;
    private float ySpeed; // 아래로 가해지는 힘
    private bool isWalk;  // 걷는 중인지 아닌지 판단

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
        // 등가속도 운동 : 속도를 시간에 따라서 점점 가속하는 경우
        ySpeed += Physics.gravity.y * Time.deltaTime;  // y에 중력을 계속 가해줌 (아래로 가해지는 힘)

        if(controller.isGrounded)  // 땅에 있을 떄는 아래로 가하는 힘을 0으로 설정해줌 (점프해야되니까)
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

		//if (controller.isGrounded)  // isGround 구리니까 그냥 groundChecker 구현하는게 나음
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
