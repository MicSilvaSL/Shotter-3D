using UnityEngine;
using System;

[CreateAssetMenu(fileName = "WeaponsHolderSO", menuName = "Weapons/Holder")]
public class WeaponsHolderSO : ScriptableObject
{
	public const int MAX_WEAPON = 3;

	[SerializeField] private ShotObjectSO[] weaponsSlots;

	public event Action<ShotObjectSO> OnChangeSlot;

	private int _id = 0;

	private void OnEnable()
	{
		if (weaponsSlots[_id] == null)
		{
			_id = Array.FindIndex(weaponsSlots, w => w != null);
		}
		else
			_id = 0;
	}

	private void OnDisable()
	{
		_id = 0;
	}

	public void Init()
	{
		_id = CheckValidSlot(0);
	}

	public ShotObjectSO GetCurrentShot()
	{
		return weaponsSlots[_id];
	}

	public void ChangeCurrentSlot(int amount = 1) 
	{
		int nextId = _id + (int)Mathf.Sign(amount);

		nextId = CheckValidId(nextId);

		_id = CheckValidSlot(nextId);

		OnChangeSlot?.Invoke(weaponsSlots[_id]);

	}

	private int CheckValidId(int id)
	{
		if (id > weaponsSlots.Length - 1)
			id = 0;
		else if (id < 0)
			id = weaponsSlots.Length - 1;

		return id;
	}
	private int CheckValidSlot(int id)
	{
		id = CheckValidId(id);

		if (weaponsSlots[id] == null)
			return CheckValidSlot(id + (int)Mathf.Sign(id - _id));
		else
			return id;
	}

}
