using NaughtyAttributes;
using UnityEngine;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal abstract class ShipBase : MonoBehaviour
	{
		protected abstract float _MaxSpeed { get; }
		protected abstract float _MaxAcceleration { get; }

		[SerializeField, ReadOnly] private Vector3 _CurrentSpeed;
		[SerializeField, ReadOnly] private float _CurrentSpeedMagnitude;

		protected virtual void Update()
		{
			_TickMovement();
			_TickDeceleration();
			_TickDebugInfo();
		}

		internal void TickAccelerationInput(Vector3 accelerationInput)
		{
			var movementAcceleration = accelerationInput * _MaxAcceleration;
			var newSpeed = _CurrentSpeed + movementAcceleration * Time.deltaTime;
			newSpeed = Vector3.ClampMagnitude(newSpeed, _MaxSpeed);
			_CurrentSpeed = newSpeed;
		}

		private void _TickMovement()
		{
			var movementTranslation = _CurrentSpeed * Time.deltaTime;
			transform.position += movementTranslation;

			if (_CurrentSpeed.magnitude > Mathf.Epsilon)
			{
				transform.rotation = Quaternion.LookRotation(Vector3.forward, _CurrentSpeed);
			}
		}

		private void _TickDeceleration()
		{
			var currentSpeedMagnitude = _CurrentSpeed.magnitude;
			if (currentSpeedMagnitude <= Mathf.Epsilon)
			{
				return;
			}

			var speedAttenuation = SpaceConfig.Instance.GlobalDeceleration * Time.deltaTime;
			if (speedAttenuation > currentSpeedMagnitude)
			{
				_CurrentSpeed = Vector3.zero;
				return;
			}

			_CurrentSpeed = _CurrentSpeed * ((currentSpeedMagnitude - speedAttenuation) / currentSpeedMagnitude);
		}

		private void _TickDebugInfo()
		{
			_CurrentSpeedMagnitude = _CurrentSpeed.magnitude;
		}
	}
}