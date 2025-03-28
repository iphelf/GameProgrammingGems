using UnityEngine;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal class ShipBase : MonoBehaviour
	{
		public float MaxSpeed = 5.0f;

		internal void HandleMovementInput(Vector3 movementInput)
		{
			var movementSpeed = movementInput * MaxSpeed;
			var movementTranslation = movementSpeed * Time.deltaTime;
			transform.position += movementTranslation;

			if (movementInput.magnitude > Mathf.Epsilon)
			{
				transform.rotation = Quaternion.LookRotation(Vector3.forward, movementInput);
			}
		}
	}
}