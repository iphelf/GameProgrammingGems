using System;
using NaughtyAttributes;
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
			[Header("Global")] public Transform Target;
			public float MaxSpeed = 3.73f;
			public float MaxAcceleration = 1;

			[Header("Accumulation weights")] public float WeightOfSeparation = 1;
			public float WeightOfAlignment = 1;
			public float WeightOfCohesion = 1;

			[Header("Perception")] public float PerceptionRange = 2;
			[Header("Separation")] public float SeparationRange = 0.5f;
			[Header("Cohesion")] public float CohesionRange = 1.5f;
		}

		private Config _Config;

		[SerializeField, ReadOnly] private float _TargetDistance;

		internal void Init(Config config)
		{
			_Config = config;
		}

		protected override void Update()
		{
			var movementInput = _SolveConstraints();
			TickAccelerationInput(movementInput);

			base.Update();

			_TickDebugInfo();
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
			var change = Vector3.zero;

			var translation = _Config.Target.position - transform.position;
			var distance = translation.magnitude;
			if (distance < _Config.SeparationRange)
			{
				var separationAcc = _Config.SeparationRange / Mathf.Max(distance, 0.01f);
				separationAcc = Mathf.Min(separationAcc, 1.0f);
				change -= translation.normalized * separationAcc;
			}

			return change;
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

		private void _TickDebugInfo()
		{
			_TargetDistance = (_Config.Target.position - transform.position).magnitude;
		}
	}
}