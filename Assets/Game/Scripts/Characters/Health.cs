using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Health : MonoBehaviour
    {
		[SerializeField] private int _baseHealth;

		private int _health;

		public event UnityAction DamageTaken;
		public event UnityAction Died;
		public event UnityAction DeathAnimationCompleted;

		private void Start()
		{
			_health = _baseHealth;
		}

		public void DealDamage(int amount)
		{
			_health = Mathf.Max(_health - amount, 0);

			if (_health == 0)
				Died?.Invoke();
			else
				DamageTaken?.Invoke();
		}

		public void Heal(int amount)
		{
			_health = Mathf.Min(_health + amount, _baseHealth);
		}

		public void Die()
		{
			gameObject.SetActive(false);
			DeathAnimationCompleted?.Invoke();
		}
	}
}