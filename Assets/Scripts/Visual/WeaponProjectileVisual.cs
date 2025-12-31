using System.Collections;
using UnityEngine;

public class WeaponProjectileVisual : MonoBehaviour
{
	[SerializeField] private ParticleSystem chargeTrails;
	[SerializeField] private ParticleSystem chargeCompletedTrails;
	
	[SerializeField] private GameObject chargeVisual;

	private void Start()
	{
		DisableAllEffects();
	}

	public void OnChargeStart()
	{
		chargeTrails.Play();
		chargeVisual.gameObject.SetActive(true);
	}

	public void OnChargeFinished()
	{
		chargeTrails.Stop();
		chargeCompletedTrails.Play();
	}

	public void DisableAllEffects()
	{
		chargeTrails.Stop();
		chargeCompletedTrails.Stop();
		chargeVisual.gameObject.SetActive(false);
	}



}
