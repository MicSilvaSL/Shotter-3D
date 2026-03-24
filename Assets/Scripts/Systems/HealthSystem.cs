using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{

	[SerializeField] private int maxHealth = 100;

	public UnityEvent<HealthSystem> OnHealthChange;
	public UnityEvent OnDeath;

	private float _currentHealth;

	public bool IsAlive => _currentHealth > 0;

	public float CurrentHealth 
	{
		get => _currentHealth;

		private set 
		{
			_currentHealth = value;
			OnHealthChange?.Invoke(this);
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

	[ContextMenu("Reset Health")]
	public void ResetHealth() => CurrentHealth = maxHealth;

	public float HealthPercent() => _currentHealth / maxHealth;

	private void OnEnable()
	{
		CurrentHealth = maxHealth;
	}

	private void OnDisable()
	{
		_currentHealth = 0;
	}

}
