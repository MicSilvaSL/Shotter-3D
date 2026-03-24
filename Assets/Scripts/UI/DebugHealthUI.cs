using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugHealthUI : MonoBehaviour
{
	[SerializeField] private HealthSystem health;

	[SerializeField] private Image hpFillBar;
	[SerializeField] private TextMeshProUGUI hpTxtDisplay;

	public void UpdateHealth(float amount)
	{
		if (hpTxtDisplay != null)
			hpTxtDisplay.text = amount.ToString();

		float percent = amount / health.MaxHealth;

		hpFillBar.fillAmount = percent;

	}
}
