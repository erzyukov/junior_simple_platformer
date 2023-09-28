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

		private bool IsGrounded
		{
			get
			{
				//float boxHeight = 0.05f;
				//Vector2 center = new Vector2(_collider.bounds.center.x, _collider.bounds.center.y - _collider.bounds.size.y / 2 - boxHeight / 2);
				//Vector2 size = new Vector2(_collider.bounds.size.x, boxHeight);

				return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0, Vector2.down, 0.01f, _walkableLayers);
			}
		}

		public void Move(float speed)
		{
			Vector3 targetVelocity = new Vector3(speed, _rigidbody.velocity.y, 0);
			_rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _currentVelocity, _movementSmoothing);
		}

		public void Jump(float jumpForce)
		{
			jumpForce = (_rigidbody.velocity.y > 0) ? jumpForce - _rigidbody.velocity.y : jumpForce;

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