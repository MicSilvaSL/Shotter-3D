using UnityEngine;

public class WeaponCameraAimCorrection : MonoBehaviour
{
	[SerializeField] private Transform weaponShotPoint;
	
	private float _maxRange = 10;

	private Camera _mainCam;
	private Ray _weaponAim;

	private void Awake()
	{
		_mainCam = Camera.main;
	}

	private void FixedUpdate()
	{
		Ray centerAnim = _mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		_weaponAim = new Ray(weaponShotPoint.position, (centerAnim.GetPoint(_maxRange) - weaponShotPoint.position).normalized);

		this.transform.forward = _weaponAim.direction;
	}
}
