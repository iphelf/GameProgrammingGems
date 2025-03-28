using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Gems1.C3S7_Flocking.Scripts
{
	internal class BoidShip : ShipBase
	{
		protected override float _MaxSpeed => _Config.MaxSpeed;
		protected override float _MaxAcceleration => _Config.MaxAcceleration;

		[Serializable]
		internal class Config
		{
			[Header("Global")] public ShipBase Target;
			public float MaxSpeed = 3.7f;
			public float MaxAcceleration = 8;

			[Header("Accumulation weights")] public float WeightOfSeparation = 1;
			public float WeightOfAlignment = 0.5f;
			public float WeightOfCohesion = 1;

			[Header("Perception")] public float PerceptionRange = 2;
			[Header("Separation")] public float SeparationRange = 0.5f;
			[Header("Alignment")] public float AlignmentRange = 0.5f;
			[Header("Cohesion")] public float CohesionRange = 1.5f;
			public float CohesionWeightOfLeader = 10.0f;
			public float CohesionWeightOfMember = 1.0f;
		}

		private Config _Config;

		[SerializeField, ReadOnly, UsedImplicitly]
		private float _TargetDistance;

		internal void Init(Config config)
		{
			_Config = config;
		}

		private void Start()
		{
			transform.position += Random.insideUnitSphere * 0.1f;
		}

		protected override void Update()
		{
			base.Update();

			_TickDebugInfo();
		}

		internal void TickConstraintsSolver(IReadOnlyList<ShipBase> vicinity)
		{
			var movementInput = _SolveConstraints(vicinity);
			TickAccelerationInput(movementInput);
		}

		private Vector3 _SolveConstraints(IReadOnlyList<ShipBase> vicinity)
		{
			var change = Vector3.zero;
			if (vicinity.Count == 0)
			{
				return change;
			}

			change += _SolveSeparationConstraint(vicinity) * _Config.WeightOfSeparation;
			change += _SolveAlignmentConstraint(vicinity) * _Config.WeightOfAlignment;
			change += _SolveCohesionConstraint(vicinity) * _Config.WeightOfCohesion;
			return change.normalized;
		}

		private Vector3 _SolveSeparationConstraint(IReadOnlyList<ShipBase> vicinity)
		{
			var change = Vector3.zero;

			foreach (var ship in vicinity)
			{
				var translation = ship.Position - Position;
				var distance = translation.magnitude;
				if (distance >= _Config.SeparationRange)
				{
					continue;
				}

				var separationAcc = _Config.SeparationRange / Mathf.Max(distance, 0.01f);
				separationAcc = Mathf.Min(separationAcc, 1.0f);
				change -= translation.normalized * separationAcc;
			}

			return change;
		}

		private Vector3 _SolveAlignmentConstraint(IReadOnlyList<ShipBase> vicinity)
		{
			if (Vector3.Dot(_Config.Target.Direction, Direction) > _Config.AlignmentRange)
			{
				return Vector3.zero;
			}

			var dirDiff = _Config.Target.Direction - Direction;
			return Vector3.ClampMagnitude(dirDiff, 1.0f);
		}

		private Vector3 _SolveCohesionConstraint(IReadOnlyList<ShipBase> vicinity)
		{
			var vicinityCenter = Vector3.zero;
			var totalWeight = 0.0f;
			foreach (var ship in vicinity)
			{
				var weight = ship is PlayerShip ? _Config.CohesionWeightOfLeader : _Config.CohesionWeightOfMember;
				vicinityCenter += ship.Position * weight;
				totalWeight += weight;
			}

			vicinityCenter /= totalWeight;
			var translation = vicinityCenter - Position;
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
			_TargetDistance = (_Config.Target.Position - Position).magnitude;
		}
	}
}