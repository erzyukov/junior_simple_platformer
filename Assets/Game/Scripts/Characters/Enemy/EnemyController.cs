using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyController : MonoBehaviour
    {
		[SerializeField] private float _speed;
		[SerializeField] private bool _inverseDirecton;
		[SerializeField] private CharacterMotion _characterMotion;
		[SerializeField] private Transform[] _patrolPoints;

		const float DistanceToSwitchTarget = 0.03f;

		private Vector3 _currentTarget;
		private int _targetIndex;

		private void Start()
		{
			if (_patrolPoints.Length == 0)
				return;

			_currentTarget = _patrolPoints[_targetIndex].position;
		}

		private void FixedUpdate()
		{
			if (_patrolPoints.Length == 0)
				return;

			Vector3 distance = _currentTarget - transform.position;

			if (distance.magnitude < DistanceToSwitchTarget)
			{
				_targetIndex++;
				
				if (_targetIndex >= _patrolPoints.Length)
					_targetIndex = 0;

				_currentTarget = _patrolPoints[_targetIndex].position;
				return;
			}


			int direction = Mathf.RoundToInt(distance.normalized.x);
			_characterMotion.SetLookDirection(direction * (_inverseDirecton? -1: 1));
			_characterMotion.Move(direction * _speed);
		}
	}
}
