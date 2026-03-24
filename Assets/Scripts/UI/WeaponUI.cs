using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
	[SerializeField] private Image weaponChargeFill;
	[SerializeField] private Color lowChargeFillColor;
	[SerializeField] private Color fullyChargeFillColor;

	public void OnChangeSlot(Weapon weaponObj)
	{
		weaponIcon.sprite = weaponObj.Data.Icon;
	}
	public void OnChargeWeapon(float amount)
	{
		weaponChargeFill.fillAmount = amount;
		weaponChargeFill.color = Color.Lerp(lowChargeFillColor, fullyChargeFillColor, amount);
	}
}
