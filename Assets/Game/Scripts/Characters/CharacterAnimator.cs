using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class CharacterAnimator : MonoBehaviour
    {
		[SerializeField] protected Animator Animator;
		[SerializeField] private Viability _characterViability;

		private const string HitAnimationParameter = "Hit";
		private const string DiedAnimationParameter = "Died";

		private void OnEnable()
		{
			_characterViability.DamageTaken += OnDamageTaken;
			_characterViability.Died += OnDied;
		}

		private void OnDisable()
		{
			_characterViability.DamageTaken -= OnDamageTaken;
			_characterViability.Died -= OnDied;
		}

		private void Die() =>
			_characterViability.Die();

		private void OnDamageTaken() =>
			Animator.SetTrigger(HitAnimationParameter);

		private void OnDied() =>
			Animator.SetTrigger(DiedAnimationParameter);
	}
}

