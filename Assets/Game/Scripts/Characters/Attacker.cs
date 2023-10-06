using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Attacker : MonoBehaviour
    {
		[SerializeField] private int _damage;
		[SerializeField] private float _attackDelay;

		private float _attackTimer;
		private List<Viability> _targets = new List<Viability>();

		private void Update()
		{
			if (_attackTimer > 0)
				_attackTimer -= Time.deltaTime;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			Viability viabilityComponent = collision.GetComponent<Viability>();

			if (viabilityComponent != null)
				_targets.Add(viabilityComponent);
		}

		private void OnTriggerStay2D(Collider2D collision)
		{
			if (_attackTimer > 0 || _targets.Count == 0)
				return;

			Viability target = _targets[0];
			target.DealDamage(_damage);
			_attackTimer = _attackDelay;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			Viability viabilityComponent = collision.GetComponent<Viability>();

			if (viabilityComponent != null)
				_targets.Remove(viabilityComponent);
		}

	}
}
