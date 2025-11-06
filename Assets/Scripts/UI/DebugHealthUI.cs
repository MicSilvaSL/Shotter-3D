using UnityEngine;
using UnityEngine.UI;

public class DebugHealthUI : MonoBehaviour
{
	[SerializeField] private Image fillImage;
	[SerializeField] private HealthHolderSO health;

	private Camera _cam;

	private void Awake()
	{
		_cam = Camera.main;
	}

	private void OnEnable()
	{
		health.OnHealthChange += UpdateHealth;
	}

	private void Update()
	{
		this.transform.LookAt(_cam.transform.position);
	}

	private void UpdateHealth(float amount)
	{
		fillImage.fillAmount = amount / health.MaxHealth;
	}

	private void OnDisable()
	{
		health.OnHealthChange -= UpdateHealth;
	}
}
