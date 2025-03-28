using System;
using UnityEngine;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal class BoidShip : ShipBase
	{
		[Serializable]
		internal class Config
		{
			public Transform Target;
			public float WeightOfSeparation = 1;
			public float WeightOfAlignment = 1;
			public float WeightOfCohesion = 1;
			public float CohesionRange = 1;
		}

		private Config _Config;

		internal void Init(Config config)
		{
			_Config = config;
		}

		private void Update()
		{
			var movementInput = _Solve();
			HandleMovementInput(movementInput);
		}

		private Vector3 _Solve()
		{
			var change = Vector3.zero;
			float totalWeight = 0;
			change += _SolveSeparation() * _Config.WeightOfSeparation;
			totalWeight += _Config.WeightOfSeparation;
			change += _SolveAlignment() * _Config.WeightOfAlignment;
			totalWeight += _Config.WeightOfAlignment;
			change += _SolveCohesion() * _Config.WeightOfCohesion;
			totalWeight += _Config.WeightOfCohesion;
			change /= totalWeight;
			return change;
		}

		// Separation
		private Vector3 _SolveSeparation()
		{
			return Vector3.zero;
		}

		// Alignment
		private Vector3 _SolveAlignment()
		{
			return Vector3.zero;
		}

		// Cohesion
		private Vector3 _SolveCohesion()
		{
			var translation = _Config.Target.position - transform.position;
			var distance = translation.magnitude;
			if (distance <= _Config.CohesionRange)
			{
				return Vector3.zero;
			}

			var direction = translation / distance;
			return direction;
		}
	}
}