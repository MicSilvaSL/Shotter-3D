using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private InputManager input;
	[SerializeField] private Transform groundCheckPos;
	[SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 2;
    [SerializeField] private float gravity = 2;
	[SerializeField] private float jumpHeight = 5;
	[SerializeField] private float groundCheckRadius = 0.45f;

    private CharacterController _controller;
	private bool _isGrounded = false;
    private Vector3 _velocity;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
    }

	private void OnEnable()
	{
		input.JumpStart += Jump;
	}
	private void OnDisable()
	{
		input.JumpStart -= Jump;
	}

	private void Jump()
	{
		if (_isGrounded)
			_velocity.y = jumpHeight * Time.deltaTime;
	}

	void Update()
    {
		Vector3 movement = this.transform.forward * input.Walk.y + this.transform.right * input.Walk.x;

		_isGrounded = Physics.CheckSphere(groundCheckPos.position, groundCheckRadius, groundLayer);
		
		_controller.Move( movement * speed * Time.deltaTime);
		
		if (!_isGrounded)
		{
			_velocity.y += -gravity * Time.deltaTime * Time.deltaTime;
		}
		else if (_isGrounded && _velocity.y < 0f)
		{ 
			_velocity.y = 0; 
		}

		_controller.Move(_velocity);
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
	}


}
