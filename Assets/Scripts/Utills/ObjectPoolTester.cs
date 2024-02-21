using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTester : MonoBehaviour
{
	private PooledObject hitEffectPrefab;

	private void Start()
	{
		hitEffectPrefab = Resources.Load<PooledObject>("Bullet");
		Manager.Pool.CreatePool("Bullet",hitEffectPrefab, 20);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Manager.Pool.GetPool("ÃÑ¾Ë", Random.insideUnitSphere * 10f, Quaternion.identity);
			//Instantiate();
			//PooledObject instance = pooler.GetPool();
		}
	}
}
