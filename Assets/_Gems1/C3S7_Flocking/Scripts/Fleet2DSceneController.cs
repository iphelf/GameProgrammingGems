using UnityEngine;
using UnityEngine.InputSystem;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal class Fleet2DSceneController : MonoBehaviour
	{
		[SerializeField] private InputActionAsset _InputActions;
		[SerializeField] private PlayerShip _PlayerShip;

		private InputAction _InputMoveAction;

		private void Start()
		{
			_InputActions.Enable();
			_InputMoveAction = _InputActions.FindAction("Move");
		}

		private void Update()
		{
			var movement = _InputMoveAction.ReadValue<Vector2>();
			_PlayerShip.TickAccelerationInput(new Vector3(movement.x, movement.y, 0));
		}
	}
}