using UnityEngine;

namespace _Gems1.C3S7_FuzzyLogic.Scripts
{
	internal class FreeMovingCar : CarBase
	{
		[SerializeField] private Transform _Car;
		[SerializeField] private FocusedCar _FocusedCar;

		private float _Speed;

		private void Update()
		{
			var relativeSpeed = _Speed - _FocusedCar.GetSpeed();
			var positionDelta = relativeSpeed * Time.deltaTime;
			var position = _Car.position;
			position.x += positionDelta;
			_Car.position = position;
		}

		internal override void SetSpeed(float speed)
		{
			_Speed = speed;
		}

		internal override float GetSpeed()
		{
			return _Speed;
		}
	}
}