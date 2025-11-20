using UnityEngine.Events;
using UnityEngine;

public class Damageble : MonoBehaviour
{
	[SerializeField] private UnityEvent<float> OnDamage;

	public void TakeDamage(float damage) 
	{
		OnDamage.Invoke(damage);
	}

}
