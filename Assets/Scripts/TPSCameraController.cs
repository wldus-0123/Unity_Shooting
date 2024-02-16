using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameraController : MonoBehaviour
{
	[SerializeField] Transform cameraRoot;
	[SerializeField] float mouseSensitivity;

	private Vector2 inputDir;
	private float xRotation;

	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
	}

	private void LateUpdate()  // 모든 진행상황이 끝난 후 후처리
	{
		xRotation -= inputDir.y * mouseSensitivity * Time.deltaTime;
		xRotation = Mathf.Clamp(xRotation, -80f, 80f);

		transform.Rotate(Vector3.up, inputDir.x * mouseSensitivity * Time.deltaTime);
		cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
	}

	private void OnLook(InputValue value)
	{
		inputDir = value.Get<Vector2>();
	}
}
