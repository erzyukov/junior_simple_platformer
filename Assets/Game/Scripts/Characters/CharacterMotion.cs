using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
	public class CharacterMotion : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private Collider2D _collider;
		[SerializeField] private LayerMask _walkableLayers;
		[SerializeField] private float _movementSmoothing;

		private bool _isFalling;

		public event UnityAction Jumped;
		public event UnityAction Falling;
		public event UnityAction Landed;
		public float CurrentSpeed => Mathf.Abs(_rigidbody.velocity.x);

		private void FixedUpdate()
		{
			HandleFalling();
		}

		public void Move(float speed)
		{
			Vector3 currentVelocity = Vector3.zero;
			Vector3 targetVelocity = new Vector3(speed, _rigidbody.velocity.y, 0);
			_rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref currentVelocity, _movementSmoothing);
		}

		public void Jump(float jumpForce)
		{
			jumpForce = (_rigidbody.velocity.y > 0) ? jumpForce - _rigidbody.velocity.y : jumpForce;

			if (IsGrounded() && jumpForce > 0)
			{
				_rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
				Jumped?.Invoke();
			}
		}

		private bool IsGrounded()
		{
			float boxHeight = 0.1f;
			Vector2 boxCenter = new Vector2(_collider.bounds.center.x, _collider.bounds.center.y - (_collider.bounds.size.y + boxHeight) / 2);
			Vector2 boxSize = new Vector2(_collider.bounds.size.x, boxHeight);
			
			return Physics2D.BoxCast(boxCenter, boxSize, 0, Vector2.down, 0, _walkableLayers);
		}

		public void SetLookDirection(int direction)
		{
			int lookDirection = direction;
			lookDirection = (lookDirection == 0) ? (int)transform.localScale.x : lookDirection;
			transform.localScale = new Vector3(lookDirection, 1, 1);
		}

		private void HandleFalling()
		{
			float fallingSpeedEdge = -0.05f;

			if (_rigidbody.velocity.y < fallingSpeedEdge && _isFalling == false)
			{
				Falling?.Invoke();
				_isFalling = true;
			}
			else if (_rigidbody.velocity.y >= fallingSpeedEdge && _isFalling)
			{
				Landed?.Invoke();
				_isFalling = false;
			}
		}
	}
}