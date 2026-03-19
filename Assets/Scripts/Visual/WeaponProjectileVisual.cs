using System.Collections;
using UnityEngine;

public class WeaponProjectileVisual : MonoBehaviour
{
	[SerializeField] private ParticleSystem chargeTrails;
	[SerializeField] private ParticleSystem chargeCompletedTrails;
	
	[SerializeField] private GameObject chargeVisual;
	[SerializeField] private Collider _collider;

	private void Start()
	{
		DisableAllEffects();
		_collider.enabled = false;
	}

	public void OnChargeStart()
	{
		_collider.enabled = true;
		chargeTrails.Play();
		chargeVisual.gameObject.SetActive(true);
	}

	public void OnChargeFinished()
	{
		_collider.enabled = false;
		chargeTrails.Stop();
		chargeCompletedTrails.Play();
	}

	public void DisableAllEffects()
	{
		_collider.enabled = false;
		chargeTrails.Stop();
		chargeCompletedTrails.Stop();
		chargeVisual.gameObject.SetActive(false);
	}



}
