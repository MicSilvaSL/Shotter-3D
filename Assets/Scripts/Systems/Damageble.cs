using UnityEngine.Events;
using UnityEngine;

public class Damageble : MonoBehaviour
{
	[SerializeField] private UnityEvent<float> OnDamageTaken;
	[SerializeField] private UnityEvent OnDamage;

	public void TakeDamage(float damage) 
	{
		OnDamageTaken.Invoke(damage);
		OnDamage.Invoke();
	}

}
