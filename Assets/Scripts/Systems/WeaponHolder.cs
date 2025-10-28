using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private InputManager input;
	[SerializeField] private WeaponsHolderSO weapons;

    private int _id = 0;

	private void OnEnable()
	{
		input.ChangeWeapon += ChangeWeapon;
		input.ShotStarted += ShotStart;
		input.ShotCanceled += ShotCancel;
	}
	private void OnDisable()
	{
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
		Debug.Log("Shot Start");
	}

	private void ChangeWeapon(int side)
	{
		weapons.ChangeCurrentSlot(side);
	}

}

