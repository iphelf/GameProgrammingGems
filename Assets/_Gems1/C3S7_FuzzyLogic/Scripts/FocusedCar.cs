using UnityEngine;

namespace _Gems1.C3S7_FuzzyLogic.Scripts
{
	internal class FocusedCar : CarBase
	{
		[SerializeField] private RoadScroller _RoadScroller;

		private float _Speed;
		private float _Position;

		private void Update()
		{
			var distance = _Speed * Time.deltaTime;
			_Position += distance;
			_RoadScroller.SetPosition(_Position);
		}

		internal override void SetSpeed(float speed)
		{
			_Speed = speed;
		}

		internal override float GetSpeed()
		{
			return _Speed;
		}

		internal override float GetPosition()
		{
			return transform.position.x;
		}
	}
}