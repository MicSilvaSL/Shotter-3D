using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ProjectileBase : MonoBehaviour
{
	[SerializeField] private float speed = 4;
    [SerializeField] private float lifeTime = 4;
	[SerializeField] private float damage = 4;

	[SerializeField] private StatusEffect effectOnContact;

	private Rigidbody _rb;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		GetComponent<Collider>().isTrigger = true;
	}

	public virtual void SetMovement(Vector3 direction)
    {
		_rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
		Destroy(this.gameObject, lifeTime);
    }
	public void SetDamage(float damage) 
	{ 
		this.damage = Mathf.Clamp(damage, 0.1f, Mathf.Infinity);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out Damageble damageble)) 
		{
			damageble.TakeDamage(damage);
		}

		if (effectOnContact == null)
			return;

		if(other.TryGetComponent(out StatusHolder statusHolder))
		{
			statusHolder.AddEffect(effectOnContact);
		}
		else
		{
			StatusHolder stats = other.AddComponent<StatusHolder>();
			stats.AddEffect(effectOnContact);
		}
	}
}
