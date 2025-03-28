using UnityEngine;

namespace _Gems1.C3S7_FuzzyLogic.Scripts
{
	internal abstract class CarBase : MonoBehaviour
	{
		internal abstract void SetSpeed(float speed);
		internal abstract float GetSpeed();
		internal abstract float GetPosition();
	}
}