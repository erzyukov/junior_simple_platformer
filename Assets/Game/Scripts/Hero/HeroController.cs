using UnityEngine;

namespace Game
{
    public class HeroController : MonoBehaviour
    {
		[SerializeField] private float _speed;
		[SerializeField] private float _jumpForce;
		[SerializeField] private CharacterMotion _characterMotion;

		private int _direction;
		private bool _isJumping;

		private void Update()
        {
			_direction = (int) Input.GetAxisRaw("Horizontal");
			
			if (Input.GetButtonDown("Jump"))
				_isJumping = true;
		}

		private void FixedUpdate()
		{
			_characterMotion.SetLookDirection(_direction);

			float speed = _speed * _direction;
			_characterMotion.Move(speed);

			if (_isJumping)
			{
				float jumpForce = _isJumping? _jumpForce: 0;
				_characterMotion.Jump(jumpForce);
				_isJumping = false;
			}
		}
	}
}