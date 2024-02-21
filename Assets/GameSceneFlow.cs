using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneFlow : MonoBehaviour
{
    [SerializeField] PooledObject bulletPrefab;
    [SerializeField] PooledObject effectPrefab;

	private void OnEnable()
	{
		Loading();
	}

	private void OnDisable()
	{
		UnLoading();
	}

	public void Loading()
	{
		Manager.Pool.CreatePool("�Ѿ�", bulletPrefab, 20);
		Manager.Pool.CreatePool("ȿ��", effectPrefab, 10);
	}

	public void UnLoading()
	{
		Manager.Pool.RemovePool("�Ѿ�");
		Manager.Pool.RemovePool("ȿ��");
	}
}
