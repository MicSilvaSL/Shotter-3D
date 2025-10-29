using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private float speed = 2;

    private CharacterController _controller;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
	}

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _controller.Move((this.transform.forward * input.Walk.y + this.transform.right * input.Walk.x) * speed * Time.deltaTime);
    }
}
