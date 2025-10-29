using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private InputManager input;
	[SerializeField] private WeaponsHolderSO weapons;

	private void Awake()
	{
		weapons.Init();
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
		
	}
	private void ShotStart()
	{
		
	}

	private void ChangeWeapon(int side)
	{
		weapons.ChangeCurrentSlot(side);
	}

}

