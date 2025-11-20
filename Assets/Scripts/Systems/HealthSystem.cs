using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

	[SerializeField] private int maxHealth = 100;
	
	private float _currentHealth;

	public event Action<float> OnHealthChange;
	public event Action OnDeath;

	public bool IsAlive => _currentHealth > 0;

	public float CurrentHealth 
	{
		get => _currentHealth;

		private set 
		{
			_currentHealth = value;
			OnHealthChange?.Invoke(_currentHealth);
		}
	}
	public int MaxHealth => maxHealth;

	public void ChangeHealth(float amount)
	{
		if (amount <= 0)
		{
			CurrentHealth = 0;
			OnDeath?.Invoke();
		}
		else if (amount >= maxHealth)
			CurrentHealth = maxHealth;
		else
			CurrentHealth = amount;
	}

	public void ReduceHealth(float reduceAmount)
	{
		ChangeHealth(_currentHealth - reduceAmount);
	}

	public void Heal(float healAmount)
	{
		ChangeHealth(_currentHealth + healAmount);
	}

	public void ResetHealth() => _currentHealth = maxHealth;

	private void OnEnable()
	{
		CurrentHealth = maxHealth;
	}

	private void OnDisable()
	{
		_currentHealth = 0;
	}

}
