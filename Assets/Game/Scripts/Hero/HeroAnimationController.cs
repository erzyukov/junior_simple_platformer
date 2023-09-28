using UnityEngine;

namespace Game
{
    public class HeroAnimationController : MonoBehaviour
    {
		[SerializeField] private Animator _animator;
		[SerializeField] private CharacterMotion _characterMotion;

		private const string SpeedAnimationParameter = "Speed";
		private const string JumpAnimationParameter = "IsJump";
		private const string FallAnimationParameter = "IsFall";
		private const string LandAnimationParameter = "IsLand";

		private void Awake()
		{
			_characterMotion.OnJump += PlayJump;
			_characterMotion.OnFall += PlayFall;
			_characterMotion.OnLand += PlayLand;
		}

		private void FixedUpdate()
		{
			_animator.SetFloat(SpeedAnimationParameter, _characterMotion.CurrentSpeed);
		}

		private void OnDestroy()
		{
			_characterMotion.OnJump -= PlayJump;
			_characterMotion.OnFall -= PlayFall;
			_characterMotion.OnLand -= PlayLand;
		}

		private void PlayJump() =>
			_animator.SetTrigger(JumpAnimationParameter);

		private void PlayFall() =>
			_animator.SetTrigger(FallAnimationParameter);

		private void PlayLand() =>
			_animator.SetTrigger(LandAnimationParameter);
	}
}
