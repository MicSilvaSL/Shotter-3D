using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthHolderSO", menuName = "Health Holder")]
public class HealthHolderSO : ScriptableObject
{

	[SerializeField] private int maxHealth = 100;
	[SerializeField] private float currentHealth;

	public event Action<float> OnHealthChange;
	public event Action OnDeath;

	public bool IsAlive => currentHealth > 0;

	public float CurrentHealth 
	{
		get => currentHealth;

		set 
		{
			bool damaged = Mathf.Sign(value - currentHealth) < 0;

			if (value <= 0) 
			{
				currentHealth = 0;
				OnDeath?.Invoke();
			}
			else if (value >= maxHealth) 
				currentHealth = maxHealth;
			else
				currentHealth = value;

			OnHealthChange?.Invoke(currentHealth);
		}
	}
	public int MaxHealth => maxHealth;

	public void ResetHealth() => currentHealth = maxHealth;

	private void OnEnable()
	{
		currentHealth = maxHealth;
	}

	private void OnDisable()
	{
		currentHealth = 0;
	}

}
