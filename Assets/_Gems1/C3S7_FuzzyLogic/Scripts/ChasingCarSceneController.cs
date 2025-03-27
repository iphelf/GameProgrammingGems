using UnityEngine;

namespace _Gems1.C3S7_FuzzyLogic.Scripts
{
	internal class ChasingCarSceneController : MonoBehaviour
	{
		[SerializeField] private CarBase _ChasingCar;
		[SerializeField] private CarBase _TargetCar;
		[SerializeField] private float _ChasingCarSpeed = 3.0f;
		[SerializeField] private float _TargetCarSpeed = 3.0f;

		private void Update()
		{
			_TargetCar.SetSpeed(_TargetCarSpeed);
			_ChasingCar.SetSpeed(_ChasingCarSpeed);
		}
	}
}