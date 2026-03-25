using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance { get; private set; }

	public List<RespawnHolder> _respawnList = new();

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(this);
	}

	private void Update()
	{
		
	}

	public void SetResapawnTimer(RespawnHolder respawn)
	{

	}

}
