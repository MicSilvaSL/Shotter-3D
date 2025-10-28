using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugView : MonoBehaviour
{
    [SerializeField] private Image image;
	[SerializeField] private TMP_Text countHealthTxt;
	[SerializeField] private HealthHolderSO health;

	private void OnEnable()
	{
		health.OnHealthChange += UpdateHealth;
	}

	private void UpdateHealth(float amount)
	{
		countHealthTxt.text = amount.ToString();
		image.fillAmount = amount / health.MaxHealth;
	}

	private void OnDisable()
	{
		health.OnHealthChange -= UpdateHealth;
	}
}
