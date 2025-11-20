using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
	[SerializeField] private PlayerData playerData;
    [SerializeField] private Image weaponIcon;
	[SerializeField] private Image weaponChargeFill;

	private void OnEnable()
	{
		WeaponController.OnChangeWeapon += OnChangeSlot;
		ProjectileWeapon.OnChargeWeapon += OnChargeWeapon;
	}

	private void OnChangeSlot(Weapon weaponObj)
	{
		weaponIcon.sprite = weaponObj.Data.Icon;
	}
	private void OnChargeWeapon(float amount)
	{
		weaponChargeFill.fillAmount = amount;
	}

	private void OnDisable()
	{
		WeaponController.OnChangeWeapon -= OnChangeSlot;
		ProjectileWeapon.OnChargeWeapon -= OnChargeWeapon;
	}
}
