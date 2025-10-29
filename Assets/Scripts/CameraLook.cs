using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private Transform playerTransform;

    private float _xRotation = 0;
    private float _yRotation = 0;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
    {
        _xRotation = input.Look.x * mouseSensitivity * Time.deltaTime;
        
        _yRotation -= input.Look.y * mouseSensitivity * Time.deltaTime;

        _yRotation = Mathf.Clamp(_yRotation, -90, 90);

        this.transform.localRotation = Quaternion.Euler(_yRotation, 0, 0);

		playerTransform.Rotate(Vector3.up, _xRotation);
        
    }
}
