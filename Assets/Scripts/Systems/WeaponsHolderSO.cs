using UnityEngine;
using System;
using System.Collections;

[CreateAssetMenu(fileName = "WeaponsHolderSO", menuName = "Weapons/Holder")]
public class WeaponsHolderSO : ScriptableObject
{
	public const int MAX_WEAPON = 3;

	[field: SerializeField] public float MaxRange { get; private set; }

	[SerializeField] private ShotObjectSO[] Slots;
	[SerializeField] private float chargeShotTime;

	public event Action<ShotObjectSO> OnChangeSlot;

	private int _id = 0;
	private bool _isCharged;
	private float _chargeProgress;

	public float ChargeProgress => _chargeProgress;
	public bool IsCharged => _isCharged;

	private void OnEnable()
	{
		if (Slots[_id] == null)
		{
			_id = Array.FindIndex(Slots, w => w != null);
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
		return Slots[_id];
	}

	#region Shoot
	public void Shoot(Vector3 shotPoint, Vector3 direction, string[] damageTags) 
	{
		Slots[_id].Shot(shotPoint, direction, damageTags);
	}
	public void ShootCharge(Vector3 shotPoint, Vector3 direction, string[] damageTags) 
	{
		if (!CanCharge() || !_isCharged)
			return;

		((ProjectileShotSO)Slots[_id]).ChangeShot(shotPoint, direction, damageTags);

	}

	public bool CanCharge()
	{
		if (Slots[_id] is not ProjectileShotSO)
			return false;

		ProjectileShotSO shot = (ProjectileShotSO)Slots[_id];

		if (shot == null || !shot.HasChargeShot())
			return false;

		return true;
	}

	public void ResetCharge()
	{
		if (!_isCharged || _chargeProgress <= 0) return;

		_isCharged = false;
		_chargeProgress = 0;
	}

	public IEnumerator EChargeShot() 
	{
		if (!CanCharge())
			yield break;

		float timeElasped = 0;
		while (timeElasped < chargeShotTime)
		{
			_chargeProgress = timeElasped / chargeShotTime;

			timeElasped += Time.deltaTime;
			yield return null;
		}

		_chargeProgress = 1;
		_isCharged = true;
	}

	#endregion

	public void ChangeCurrentSlot(int amount = 1) 
	{
		int nextId = _id + (int)Mathf.Sign(amount);

		nextId = CheckValidId(nextId);

		_id = CheckValidSlot(nextId);

		OnChangeSlot?.Invoke(Slots[_id]);

	}

	private int CheckValidId(int id)
	{
		if (id > Slots.Length - 1)
			id = 0;
		else if (id < 0)
			id = Slots.Length - 1;

		return id;
	}
	private int CheckValidSlot(int id)
	{
		id = CheckValidId(id);

		if (Slots[id] == null)
			return CheckValidSlot(id + (int)Mathf.Sign(id - _id));
		else
			return id;
	}

}
