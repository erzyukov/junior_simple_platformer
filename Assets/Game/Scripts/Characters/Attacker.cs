using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Attacker : MonoBehaviour
    {
		[SerializeField] private int _damage;
		[SerializeField] private float _attackDelay;

		private float _attackTimer;
		private List<Health> _targets = new List<Health>();

		private void Update()
		{
			if (_attackTimer > 0)
				_attackTimer -= Time.deltaTime;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			Health viabilityComponent = collision.GetComponent<Health>();

			if (viabilityComponent != null)
				_targets.Add(viabilityComponent);
		}

		private void OnTriggerStay2D(Collider2D collision)
		{
			if (_attackTimer > 0 || _targets.Count == 0)
				return;

			Health target = _targets[0];
			target.DealDamage(_damage);
			_attackTimer = _attackDelay;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			Health viabilityComponent = collision.GetComponent<Health>();

			if (viabilityComponent != null)
				_targets.Remove(viabilityComponent);
		}

	}
}
