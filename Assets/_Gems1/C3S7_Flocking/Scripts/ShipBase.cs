using UnityEngine;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal abstract class ShipBase : MonoBehaviour
	{
		protected abstract float _MaxSpeed { get; }

		internal void HandleMovementInput(Vector3 movementInput)
		{
			var movementSpeed = movementInput * _MaxSpeed;
			var movementTranslation = movementSpeed * Time.deltaTime;
			transform.position += movementTranslation;

			if (movementInput.magnitude > Mathf.Epsilon)
			{
				transform.rotation = Quaternion.LookRotation(Vector3.forward, movementInput);
			}
		}
	}
}