using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Viability : MonoBehaviour
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
			_health -= amount;

			if (_health <= 0)
			{
				_health = 0;
				Died?.Invoke();
			}
			else
			{
				DamageTaken?.Invoke();
			}
		}

		public void Heal(int amount)
		{
			_health += amount;

			if (_health > _baseHealth)
				_health = _baseHealth;
		}

		public void Die()
		{
			gameObject.SetActive(false);
			DeathAnimationCompleted?.Invoke();
		}
	}
}