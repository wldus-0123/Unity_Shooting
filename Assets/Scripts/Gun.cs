using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] Transform muzzlePoint;
	[SerializeField] int damage;
	[SerializeField] float maxDistance;
	[SerializeField] LayerMask layerMask;
	[SerializeField] ParticleSystem muzzleFlash;

	public void Fire()
	{	
		muzzleFlash.Play();

		if(Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance, layerMask))
		{
			// ���� ���� �� ����
			Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.red, 0.1f);
			IDamagable target = hitInfo.collider.GetComponent<IDamagable>();  // GetComponent�� �������̽��� ȣ���� �� ����
			

			target?.TakeDamage(damage);
		}
		else
		{
			// ���� ���� �� �ƹ��͵� �ȸ���
			Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.1f);
		}
	}
}
