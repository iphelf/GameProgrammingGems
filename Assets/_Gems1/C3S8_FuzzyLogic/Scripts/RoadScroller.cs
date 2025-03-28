using UnityEngine;

namespace _Gems1.C3S7_FuzzyLogic.Scripts
{
	internal class RoadScroller : MonoBehaviour
	{
		[SerializeField] private float _ViewportWidth;
		[SerializeField] private float _SegmentWidth;

		[SerializeField] private Transform[] _Segments;

		[SerializeField] private float _Position;

		private void Update()
		{
			_MoveToPosition(_Position);
		}

		private void _MoveToPosition(float x)
		{
			x = (x % _SegmentWidth + _SegmentWidth) % _SegmentWidth;
			for (var i = 0; i < _Segments.Length; ++i)
			{
				var segment = _Segments[i];
				var positionX = -0.5f * _ViewportWidth + (0.5f + i) * _SegmentWidth - x;
				var position = segment.localPosition;
				position.x = positionX;
				segment.localPosition = position;
			}
		}

		internal void SetPosition(float position)
		{
			_Position = position;
		}
	}
}