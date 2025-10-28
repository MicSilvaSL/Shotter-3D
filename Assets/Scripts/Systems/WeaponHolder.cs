using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private InputManager input;
	[SerializeField] private WeaponsHolderSO weapons;
	[SerializeField] private HealthHolderSO health;

    private int _id = 0;

	private void Awake()
	{
		weapons.Init();
		health.ResetHealth();
	}

	private void OnEnable()
	{
		if (weapons != null) weapons.Init();

		if (input == null) return;

		input.ChangeWeapon += ChangeWeapon;
		input.ShotStarted += ShotStart;
		input.ShotCanceled += ShotCancel;
	}
	private void OnDisable()
	{
		if (input == null) return;

		input.ChangeWeapon -= ChangeWeapon;
		input.ShotStarted -= ShotStart;
		input.ShotCanceled -= ShotCancel;
	}

	private void ShotCancel()
	{
		Debug.Log("Shot end");
	}
	private void ShotStart()
	{
		health.CurrentHealth -= 0.5f;
	}

	private void ChangeWeapon(int side)
	{
		weapons.ChangeCurrentSlot(side);
	}

}

