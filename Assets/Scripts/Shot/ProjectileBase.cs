using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBase : MonoBehaviour
{
	[SerializeField] private float speed = 4;
    [SerializeField] private float lifeTime = 4;

	private Rigidbody _rb;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
	}

	public void SetMovement(Vector3 direction)
    {
		_rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
		Destroy(this.gameObject, lifeTime);
    }
}
