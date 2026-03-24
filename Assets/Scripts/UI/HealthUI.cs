using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	[SerializeField] private Image hpFillBar;
	[SerializeField] private TextMeshProUGUI hpTxtDisplay;

	public void UpdateHealth(HealthSystem health)
	{
		if (hpTxtDisplay != null)
			hpTxtDisplay.text = health.CurrentHealth.ToString();

		float percent = health.HealthPercent();

		hpFillBar.fillAmount = percent;
		hpFillBar.color = Color.Lerp(Color.indianRed, Color.cornflowerBlue, percent);

	}

}
