using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	private Dictionary<string, ObjectPooler> poolDic = new Dictionary<string, ObjectPooler>();


	// 1. 오브젝트 풀 생성
	public void CreatePool(string name, PooledObject prefab, int size)
	{
		GameObject poolObject = new GameObject($"Pool_{name}");  // 빈 게임 오브젝트 만들기
		ObjectPooler pooler = poolObject.AddComponent<ObjectPooler>();
		pooler.CreatePool(prefab, size);

		poolDic.Add(name, pooler);

		
	}


	// 2. 오브젝트 풀 제거
	public void RemovePool(string name)
	{
		ObjectPooler pooler = poolDic[name];
		Destroy(pooler.gameObject);

		poolDic.Remove(name);
	}


	// 3. 오브젝트 풀에서 인스턴스 가져오기
	public PooledObject GetPool(string name, Vector3 position, Quaternion rotation)
	{
		return poolDic[name].GetPool(position, rotation);
	}
}
