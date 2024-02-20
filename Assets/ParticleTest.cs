using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour, IDamagable
{
	[SerializeField] ParticleSystem particle;
	public void TakeDamage(int damage)
	{
		Instantiate(particle, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
