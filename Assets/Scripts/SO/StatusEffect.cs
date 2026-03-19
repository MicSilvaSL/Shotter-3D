using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
	[SerializeField] private float duration = 1;

	public float Duration => Mathf.Abs(duration);

	public abstract void SetStatusEffect(Transform target);
	public abstract void ResetStatusEffect(Transform target);

}
