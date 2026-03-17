using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlowMovementOnContact : MonoBehaviour
{
	[SerializeField, Range(0.1f, 1)] private float movementCapPercent;
	[SerializeField] private float slowedTime = 1;
	[SerializeField] private float timeRecoveryTime = 0;

	private Collider _collider;

	private void Awake()
	{
		slowedTime = Mathf.Abs(slowedTime);
		timeRecoveryTime = Mathf.Abs(timeRecoveryTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IMovePosition movable))
		{
			StopAllCoroutines();
			StartCoroutine(SlowedTimer(movable));
		}
	}

	private IEnumerator SlowedTimer(IMovePosition moveble)
	{
		if (moveble == null) yield break;

		moveble.SetMovementPercent(movementCapPercent);

		yield return new WaitForSeconds(Mathf.Abs(slowedTime));

		if (Mathf.Abs(timeRecoveryTime) > 0) 
		{
			float timerElapsed = 0;

			while(timerElapsed >= Mathf.Abs(timeRecoveryTime))
			{
				timerElapsed += Time.deltaTime;

				float t = timerElapsed / Mathf.Abs(timeRecoveryTime);

				moveble.SetMovementPercent(Mathf.Lerp(movementCapPercent, 1, t));
			}

		}

		moveble.SetMovementPercent();

	}

}
