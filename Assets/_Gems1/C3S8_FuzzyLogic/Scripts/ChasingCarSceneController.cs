using NaughtyAttributes;
using UnityEngine;

namespace _Gems1.C3S7_FuzzyLogic.Scripts
{
	internal class ChasingCarSceneController : MonoBehaviour
	{
		[SerializeField] private CarBase _ChasingCar;
		[SerializeField] private CarBase _TargetCar;
		[SerializeField] private float _ChasingCarSpeed = 3.0f;
		[SerializeField] private float _TargetCarSpeed = 3.0f;
		[SerializeField] private float _InferTimeSpan = 1.0f;
		[SerializeField, ReadOnly] private float _InferredSpeedScale;

		private FuzzyLogicSystem _FuzzyLogicSystem;

		private void Start()
		{
			_FuzzyLogicSystem = new FuzzyLogicSystem();
		}

		private void Update()
		{
			_TargetCar.SetSpeed(_TargetCarSpeed);
			_ChasingCar.SetSpeed(_ChasingCarSpeed);
			_AdjustChasingCarSpeed();
		}

		private void _AdjustChasingCarSpeed()
		{
			var distance = _TargetCar.GetPosition() - _ChasingCar.GetPosition();
			var relativeSpeed = _TargetCar.GetSpeed() - _ChasingCar.GetSpeed();
			var distanceDelta = relativeSpeed * _InferTimeSpan;
			if (distance > 0)
			{
				_InferredSpeedScale = _FuzzyLogicSystem.InferSpeedScale(distance, distanceDelta);
			}
			else
			{
				_InferredSpeedScale = _FuzzyLogicSystem.InferSpeedScale(-distance, -distanceDelta);
				_InferredSpeedScale = 1 / _InferredSpeedScale;
			}

			_ChasingCarSpeed *= _InferredSpeedScale;
		}
	}
}