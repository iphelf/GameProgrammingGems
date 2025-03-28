using UnityEngine;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal class PlayerShip : ShipBase
	{
		[SerializeField] private float _MaxMoveSpeed = 5.0f;
		protected override float _MaxSpeed => _MaxMoveSpeed;
	}
}