using UnityEngine;
using UnityEngine.UI;

public class DebugView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private WeaponsHolderSO weapons;

	private void OnEnable()
	{
		weapons.OnChangeSlot += ChangeSlot;
	}

	private void OnDisable()
	{
		weapons.OnChangeSlot -= ChangeSlot;
	}

	private void ChangeSlot(ShotObjectSO shotObj)
    {
        image.sprite = shotObj.Icon;
    }
}
