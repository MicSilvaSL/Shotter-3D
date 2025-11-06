using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private WeaponsHolderSO weapon;
    [SerializeField] private Image weaponIcon;

	private void OnEnable()
	{
		weapon.OnChangeSlot += OnChangeSlot;
	}

	private void Start()
	{
		weaponIcon.sprite = weapon.GetCurrentShot().Icon;
	}

	private void OnChangeSlot(ShotObjectSO shotObj)
	{
		weaponIcon.sprite = shotObj.Icon;
	}

	private void OnDisable()
	{
		weapon.OnChangeSlot -= OnChangeSlot;
	}
}
