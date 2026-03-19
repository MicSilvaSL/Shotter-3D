using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
	[SerializeField] private float duration = 1;
	[SerializeField] private GameObject feedbackPrefab;

	public float Duration => Mathf.Abs(duration);
	public GameObject FeedbackPrefab => feedbackPrefab;

	public abstract void SetStatusEffect(Transform target);
	public abstract void ResetStatusEffect(Transform target);

}
