using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugView : MonoBehaviour
{
    [SerializeField] private Image image;
	[SerializeField] private WeaponsHolderSO weapons;

	private void Update()
	{
		image.fillAmount = weapons.ChargeProgress;
	}
}
