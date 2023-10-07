using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
		[SerializeField] private float _speed;
		[SerializeField] private bool _inverseDirecton;
		[SerializeField] private CharacterMotion _characterMotion;
		[SerializeField] private Transform[] _patrolPoints;
		[SerializeField] private float _pursuitDistance;

		private const float DistanceToSwitchTarget = 0.03f;

		private Vector3 _currentPatrolTarget;
		private int _targetIndex;
		private Transform _heroTarget;

		private void Start()
		{
			_heroTarget = FindObjectOfType<Hero>().transform;

			if (_patrolPoints.Length == 0)
				return;

			_currentPatrolTarget = _patrolPoints[_targetIndex].position;
		}

		private void FixedUpdate()
		{
			if (_patrolPoints.Length == 0)
				return;

			float distanceToHero = Vector3.Distance(transform.position, _heroTarget.position);

			Vector3 currentTargetDistance;

			if (distanceToHero < _pursuitDistance)
			{
				currentTargetDistance = _heroTarget.position - transform.position;
			}
			else
			{
				currentTargetDistance = _currentPatrolTarget - transform.position;

				if (currentTargetDistance.magnitude < DistanceToSwitchTarget)
				{
					_targetIndex++;

					if (_targetIndex >= _patrolPoints.Length)
						_targetIndex = 0;

					_currentPatrolTarget = _patrolPoints[_targetIndex].position;

					return;
				}
			}

			int direction = Mathf.RoundToInt(currentTargetDistance.normalized.x);
			_characterMotion.SetLookDirection(direction * (_inverseDirecton? -1: 1));
			_characterMotion.Move(direction * _speed);
		}
	}
}
