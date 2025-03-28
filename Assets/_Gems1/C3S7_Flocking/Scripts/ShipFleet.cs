using System.Collections.Generic;
using UnityEngine;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal class ShipFleet : MonoBehaviour
	{
		[SerializeField] private BoidShip.Config _BoidShipConfig = new();
		[SerializeField] private List<BoidShip> _BoidShips = new();

		private void Awake()
		{
			foreach (var boidShip in _BoidShips)
			{
				boidShip.Init(_BoidShipConfig);
			}
		}
	}
}