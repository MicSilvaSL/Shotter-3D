using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	[SerializeField] private HealthSystem health;

	[SerializeField] private Image hpFillBar;
	[SerializeField] private TextMeshProUGUI hpTxtDisplay;

	private void OnEnable()
	{
		health.OnHealthChange += UpdateHealth;
	}

	private void UpdateHealth(float amount)
	{
		if (hpTxtDisplay != null)
			hpTxtDisplay.text = amount.ToString();

		hpFillBar.fillAmount = amount / health.MaxHealth;
	}

	private void OnDisable()
	{
		health.OnHealthChange -= UpdateHealth;
	}
}
