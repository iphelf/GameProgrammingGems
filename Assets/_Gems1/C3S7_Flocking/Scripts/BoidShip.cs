using System;
using UnityEngine;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal class BoidShip : ShipBase
	{
		protected override float _MaxSpeed => _Config.MaxSpeed;
		protected override float _MaxAcceleration => _Config.MaxAcceleration;

		[Serializable]
		internal class Config
		{
			public Transform Target;
			public float MaxSpeed = 3.73f;
			public float MaxAcceleration = 1;
			public float WeightOfSeparation = 1;
			public float WeightOfAlignment = 1;
			public float WeightOfCohesion = 1;
			public float PerceptionRange = 2;
			public float CohesionRange = 1;
		}

		private Config _Config;

		internal void Init(Config config)
		{
			_Config = config;
		}

		protected override void Update()
		{
			var movementInput = _SolveConstraints();
			TickAccelerationInput(movementInput);

			base.Update();
		}

		private Vector3 _SolveConstraints()
		{
			if ((_Config.Target.position - transform.position).magnitude > _Config.PerceptionRange)
			{
				return Vector3.zero;
			}

			var change = Vector3.zero;
			change += _SolveSeparationConstraint() * _Config.WeightOfSeparation;
			change += _SolveAlignmentConstraint() * _Config.WeightOfAlignment;
			change += _SolveCohesionConstraint() * _Config.WeightOfCohesion;
			return change.normalized;
		}

		private Vector3 _SolveSeparationConstraint()
		{
			return Vector3.zero;
		}

		private Vector3 _SolveAlignmentConstraint()
		{
			return Vector3.zero;
		}

		private Vector3 _SolveCohesionConstraint()
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