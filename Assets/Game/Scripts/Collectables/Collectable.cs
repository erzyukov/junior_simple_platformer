using UnityEngine;

namespace Game
{
    public class Collectable : MonoBehaviour
    {
		[SerializeField] private Animator _animator;

		private const string CollectedAnimationParameter = "Collected";

		virtual protected void OnTriggerEnter2D(Collider2D collision) =>
			_animator.SetTrigger(CollectedAnimationParameter);

		private void SetAsCollected() =>
			gameObject.SetActive(false);
	}
}
