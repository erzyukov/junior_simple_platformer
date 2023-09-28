using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
	public class CharacterMotion : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private Collider2D _collider;
		[SerializeField] private LayerMask _walkableLayers;
		[SerializeField] private float _groundDetectDistance;
		[SerializeField] private float _movementSmoothing;

		private Vector3 _currentVelocity;

		private bool IsGrounded =>
			Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0, Vector2.down, _groundDetectDistance, _walkableLayers);

		public void Move(float speed)
		{
			Vector3 targetVelocity = new Vector3(speed, _rigidbody.velocity.y, 0);
			_rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _currentVelocity, _movementSmoothing);
		}

		public void Jump(float jumpForce)
		{
			if (IsGrounded && jumpForce > 0)
				_rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}

		public void SetLookDirection(int direction)
		{
			int lookDirection = direction;
			lookDirection = (lookDirection == 0) ? (int)transform.localScale.x : lookDirection;
			transform.localScale = new Vector3(lookDirection, 1, 1);
		}
	}
}