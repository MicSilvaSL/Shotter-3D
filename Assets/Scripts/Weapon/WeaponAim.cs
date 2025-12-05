using UnityEngine;

public class WeaponAim : MonoBehaviour
{
	[SerializeField] private Transform weaponShotPoint;
	[SerializeField] private float maxRange = 5;

	private Camera _mainCam;
	private Ray _weaponAim;
	private int _layerTraget = 0;

	public Ray Aim => _weaponAim;
	public int TargetLayer => _layerTraget;
	public float MaxRange => maxRange;

	private void Awake()
	{
		_mainCam = Camera.main;
	}

	private void Start()
	{
		_layerTraget = ~(1 << this.gameObject.layer);
	}

	private void FixedUpdate()
	{
		Ray centerAnim = _mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		_weaponAim = new Ray(weaponShotPoint.position, (centerAnim.GetPoint(maxRange) - weaponShotPoint.position).normalized);

		//Debug.DrawRay(_weaponRay.origin, _weaponRay.direction * maxRange, Color.blue);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		//Gizmos.DrawRay(_weaponAim.origin, _weaponAim.direction * maxRange);

	}
}
