using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPooler pooler;
	[SerializeField] bool autoRelease;
	[SerializeField] float releaseTime;

	public void OnEnable()
	{
		if(autoRelease)
		{
			releaseRoutine = StartCoroutine(ReleaseRoutine());
		}
		
	}

	private void OnDisable()
	{
		if (autoRelease)
		{
			StopCoroutine(releaseRoutine);
		}
	}

	private void Update()
	{
		if(Input.GetKeyUp(KeyCode.A)) 
		{
			Release();
		}
	}


	Coroutine releaseRoutine;

	IEnumerator ReleaseRoutine()
	{
		yield return new WaitForSeconds(releaseTime);
		Release();
	}
	public void Release()
    {
		if(pooler != null)
		{
			pooler.ReturnPool(this);
		}
		else
		{
			Destroy(gameObject);
		}
    }
}
