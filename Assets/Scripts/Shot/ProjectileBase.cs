using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ProjectileBase : MonoBehaviour
{
	[SerializeField] private float speed = 4;
    [SerializeField] private float lifeTime = 4;
	[SerializeField] private float damage = 4;

	private Rigidbody _rb;
	private string[] _targetsTags;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		GetComponent<Collider>().isTrigger = true;
	}

	public void SetMovement(Vector3 direction)
    {
		_rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
		Destroy(this.gameObject, lifeTime);
    }

	public void SetTargets(params string[] targets) 
	{
		_targetsTags = targets;
	}

	private void OnTriggerEnter(Collider other)
	{
		foreach (string target in _targetsTags)
		{
			if (other.CompareTag(target)) 
			{
				if (other.gameObject.TryGetComponent(out IDamageble damageble)) 
				{
					damageble.TakeDamage(this.transform.position, damage);
				}

				Destroy(this.gameObject);
				break;
			}
		}
	}
}
