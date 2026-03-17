using UnityEngine;

public interface IMovePosition
{
	void MoveDirection(Vector3 direction);

	void SetMovementPercent(float percent = 1);
}