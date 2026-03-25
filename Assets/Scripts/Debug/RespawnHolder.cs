using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RespawnHolder : MonoBehaviour
{
    [SerializeField] private float timeToRespawn;

    private Vector3 _spwanPosition;
    
    public UnityEvent OnRespwan;
	public UnityEvent OnDespawn;

	public float TimeToRespawn => timeToRespawn;

	private void Awake()
	{
        _spwanPosition = transform.position;
	}

	public void Respawn()
    {
        this.transform.position = _spwanPosition;
        OnRespwan?.Invoke();
    }

    public void Despawn()
    {
		OnDespawn?.Invoke();
		if (this.TryGetComponent(out StatusHolder status))
        {
            status.ResetEffect();
        }

	}

    public async void TriggerRespawnTimer()
    {
        Despawn();
        await Awaitable.WaitForSecondsAsync(timeToRespawn);
        Respawn();
    }
}
