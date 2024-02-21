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
	[SerializeField] ParticleSystem hitEffect;

	public void Fire()
	{	
		muzzleFlash.Play();

		if(Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance, layerMask))
		{
			// 총을 쐈을 때 맞음
			Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.red, 0.1f);

			IDamagable target = hitInfo.collider.GetComponent<IDamagable>();  // GetComponent는 인터페이스도 호출할 수 있음
		
			target?.TakeDamage(damage);

			ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
			effect.transform.parent = hitInfo.transform;

			Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
			if(rigid != null)
			{
				rigid.AddForceAtPosition(-hitInfo.normal*10f, hitInfo.point, ForceMode.Impulse);
			}
		}
		else
		{
			// 총을 쐈을 때 아무것도 안맞음
			Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.1f);
		}
	}
}
