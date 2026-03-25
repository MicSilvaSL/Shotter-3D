using UnityEngine;

public class MovePathways : MonoBehaviour , IMovePosition
{
    [SerializeField] private Vector3[] points = new Vector3[2];
	[SerializeField] private float speed;

    private int _index = 0;
	private float _minDistance = 1.2f;
	private float _internalSpeed;
	private float _movePercent = 1;

	private bool _canMove = true;

	private void FixedUpdate()
	{
		if (!_canMove) return;

		float distanceToTarget = Vector3.Distance(this.transform.position, points[_index]);

		//Debug.Log(_index);

		if (distanceToTarget <= _minDistance)
			SetNextIndex();

		MoveDirection(points[_index] - this.transform.position);
	}

	private void SetNextIndex()
	{
		_index++;
		if (_index >= points.Length)
			_index = 0;

	}

	public void MoveDirection(Vector3 direction)
	{
		//Debug.DrawRay(this.transform.position, direction * 4, Color.antiqueWhite);
		_internalSpeed = speed * _movePercent;
		this.transform.position += direction.normalized * _internalSpeed * Time.deltaTime;
	}

	public void SetMovementPercent(float percent = 1) => _movePercent = Mathf.Clamp(Mathf.Abs(percent), 0.1f, 1);

	public void TriggerMovement(bool canMove) => _canMove = canMove;

	private void OnDrawGizmos()
	{
		if (points.Length <= 0) return;

		float radius = 0.3f;

		foreach (Vector3 point in points) 
		{ 
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(point, radius);
		}
	}

}
