using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthHolderSO", menuName = "Health Holder")]
public class HealthHolderSO : ScriptableObject
{

	[SerializeField] private int maxHealth = 100;
	[SerializeField] private float currentHealth = 100;

	public event Action<float> OnHealthChange;

	public float CurrentHealth 
	{
		get => currentHealth;

		set 
		{
			bool damaged = Mathf.Sign(value - currentHealth) < 0;

			if (value < 0)
				currentHealth = 0;
			else if (value >= maxHealth) 
				currentHealth = maxHealth;
			else
				currentHealth = value;

			if (damaged)

			OnHealthChange?.Invoke(currentHealth);
		}
	}
	public int MaxHealth => maxHealth;

	public void ResetHealth() => currentHealth = maxHealth;
	
}
