using System;
using System.Collections;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private InputManager input;
	[SerializeField] private WeaponsHolderSO weapons;
	[SerializeField] private Transform weaponShotPoint;
	[SerializeField] private float weaponMaxRange;
	[SerializeField] private float chargeShotTime;
	

	private Camera _mainCam;
	private Coroutine e_chargeShot;
	private ProjectileShotSO _chargeShot;

	private Vector3 _shotPoint = new Vector3(0, -100, 0);
	private Vector3 _weaponDirection = new Vector3(0, 0, 0);
	private Ray _shotAim;

	private int _layer = 0;

	private float _lastTimeShot = 0;

	private bool _isCharged = false;

	private void Awake()
	{
		weapons.Init();
	}

	private void OnEnable()
	{
		if (weapons != null) weapons.Init();

		if (input == null) return;

		input.ChangeWeapon += ChangeWeapon;
		input.ShotStarted += ShotStart;
		input.ShotCanceled += ShotCancel;
	}
	private void OnDisable()
	{
		if (input == null) return;

		input.ChangeWeapon -= ChangeWeapon;
		input.ShotStarted -= ShotStart;
		input.ShotCanceled -= ShotCancel;
	}

	private void Start()
	{
		_mainCam = Camera.main;
		_layer = ~(1 << 3);
		//Debug.Log(Convert.ToString(_layer, 2).PadLeft(32, '0'));
	}

	private void ShotCancel()
	{
		if (e_chargeShot != null)
			StopCoroutine(e_chargeShot);

		if (_isCharged && _chargeShot != null)
		{
			_chargeShot.ChangeShot(weaponShotPoint.position, _weaponDirection);
		}

		_isCharged = false;
		_chargeShot = null;
	}
	private void ShotStart()
	{
		if (_lastTimeShot > Time.time)
			return;

		ShotObjectSO shot = weapons.GetCurrentShot();

		_lastTimeShot = Time.time + shot.FireRate;

		if (e_chargeShot != null)
			StopCoroutine (e_chargeShot);

		shot.Shot(weaponShotPoint.position, _weaponDirection);

		e_chargeShot = StartCoroutine(EChargeShot(shot));
	}

	private IEnumerator EChargeShot(ShotObjectSO shotObj)
	{
		if (shotObj is not ProjectileShotSO)
		{
			yield break;
		}

		ProjectileShotSO chargeShot = (ProjectileShotSO)shotObj;

		if (chargeShot == null || !chargeShot.HasChargeShot())
			yield break;

		float timeElasped = 0;
		while (timeElasped < chargeShotTime)
		{
			timeElasped += Time.deltaTime;
			yield return null;
		}

		_isCharged = true;
		_chargeShot = chargeShot;

	}

	private void FixedUpdate()
	{
		_shotAim = _mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

		if (Physics.Raycast(_shotAim, out RaycastHit hitInfo, weaponMaxRange, _layer))
		{
			_shotPoint = hitInfo.point;
		}
		else 
		{
			_shotPoint = _shotAim.GetPoint(weaponMaxRange);
		}

		_weaponDirection = (_shotPoint - weaponShotPoint.position).normalized;

		Debug.DrawRay(weaponShotPoint.position, _weaponDirection * weaponMaxRange, Color.blue);

	}

	private void ChangeWeapon(int side)
	{
		weapons.ChangeCurrentSlot(side);
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(_shotAim.origin, _shotAim.direction * weaponMaxRange);


		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(_shotPoint, 0.2f);
		
	}
}

