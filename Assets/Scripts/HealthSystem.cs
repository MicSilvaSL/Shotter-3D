using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageble
{

    [SerializeField] private HealthHolderSO healthSO;

	private void OnEnable()
	{
		healthSO.OnDeath += Death;
	}

	private void Start()
	{
		healthSO.ResetHealth();
	}

	void Update()
    {
        
    }

	private void OnDisable()
	{
		healthSO.OnDeath -= Death;
	}


	public void TakeDamage(Vector3 damgePoint, float damage)
	{
		//TODO: fazer o knockback
		healthSO.CurrentHealth -= damage;
	}
	private void Death()
	{
		Destroy(this.gameObject);
	}
}
