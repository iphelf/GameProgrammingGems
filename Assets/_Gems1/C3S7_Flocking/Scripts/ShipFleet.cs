using System.Collections.Generic;
using UnityEngine;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal class ShipFleet : MonoBehaviour
	{
		[SerializeField] private BoidShip.Config _BoidShipConfig = new();
		[SerializeField] private List<BoidShip> _BoidShips = new();

		private readonly List<ShipBase> _FoundVicinityShips = new();

		private void Awake()
		{
			foreach (var boidShip in _BoidShips)
			{
				boidShip.Init(_BoidShipConfig);
			}
		}

		private void Update()
		{
			foreach (var boidShip in _BoidShips)
			{
				_FindVicinityShips(boidShip, _FoundVicinityShips);
				boidShip.TickConstraintsSolver(_FoundVicinityShips);
			}
		}

		private void _FindVicinityShips(ShipBase center, in List<ShipBase> vicinityShips)
		{
			vicinityShips.Clear();

			var sqrPerceptionRange = _BoidShipConfig.PerceptionRange * _BoidShipConfig.PerceptionRange;
			if (_BoidShipConfig.Target != center && IsInVicinity(_BoidShipConfig.Target))
			{
				vicinityShips.Add(_BoidShipConfig.Target);
			}

			foreach (var boidShip in _BoidShips)
			{
				if (boidShip != center && IsInVicinity(boidShip))
				{
					vicinityShips.Add(boidShip);
				}
			}

			return;

			bool IsInVicinity(ShipBase otherShip)
			{
				var sqrDistance = (otherShip.Position - center.Position).sqrMagnitude;
				return sqrDistance <= sqrPerceptionRange;
			}
		}
	}
}