using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Manager Config", menuName ="Input/Input Manager")]
public class InputManager : ScriptableObject, PlayerCoreInput.IPlayerActions
{
	private PlayerCoreInput _input;

	public Vector2 Walk {  get; private set; }
	public Vector2 Look { get; private set; }
	public bool Jump { get; private set; }

	public event Action JumpStart;
	public event Action JumpCancel;

	public event Action<int> ChangeWeapon;

	public event Action ShotStarted;
	public event Action ShotCanceled;

	private void OnEnable()
	{
		if (_input == null) 
		{
			_input = new PlayerCoreInput();
			_input.Player.SetCallbacks(this);
			
		}

		_input.Enable();
	}

	private void OnDisable()
	{
		_input.Disable();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		Walk = context.ReadValue<Vector2>();
	}
	public void OnLook(InputAction.CallbackContext context)
	{
		Look = context.ReadValue<Vector2>();
	}

	public void OnChangeWeapon(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
			ChangeWeapon?.Invoke((int)context.ReadValue<float>());
	}

	public void OnFire(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
			ShotStarted?.Invoke();
		else if (context.phase == InputActionPhase.Canceled)
			ShotCanceled?.Invoke();

	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started) 
		{
			JumpStart?.Invoke();
			Jump = true;
		}
		else if (context.phase == InputActionPhase.Canceled) 
		{
			JumpCancel?.Invoke();
			Jump = false;
		}
	}
}
