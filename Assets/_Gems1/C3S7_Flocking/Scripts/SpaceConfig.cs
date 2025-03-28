using UnityEngine;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal class SpaceConfig : MonoBehaviour
	{
		[SerializeField] private float _GlobalDeceleration = 0.3f;

		public float GlobalDeceleration => _GlobalDeceleration;

		internal static SpaceConfig Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
		}
	}
}