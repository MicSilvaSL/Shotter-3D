using UnityEngine;

[CreateAssetMenu(fileName = "Slow Effect", menuName = "Status Effect/Slow")]
public class SlowStatusEffect : StatusEffect
{
	[SerializeField, Range(0.1f, 1)] private float movementCapPercent;

	public override void ResetStatusEffect(Transform target)
	{
		if (target.TryGetComponent(out IMovePosition movable))
		{
			movable.SetMovementPercent();
		}
	}

	public override void SetStatusEffect(Transform target)
	{
		if (target.TryGetComponent(out IMovePosition movable))
		{
			movable.SetMovementPercent(movementCapPercent);
		}
	}
}
