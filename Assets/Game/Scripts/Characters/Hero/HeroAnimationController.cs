using UnityEngine;

namespace Game
{
    public class HeroAnimationController : CharacterAnimator
	{
		[SerializeField] private CharacterMotion _characterMotion;

		private const string SpeedAnimationParameter = "Speed";
		private const string JumpAnimationParameter = "Jump";
		private const string FallAnimationParameter = "Fall";
		private const string LandAnimationParameter = "Land";

		private void Awake()
		{
			_characterMotion.OnJump += PlayJump;
			_characterMotion.OnFall += PlayFall;
			_characterMotion.OnLand += PlayLand;
		}

		private void FixedUpdate()
		{
			Animator.SetFloat(SpeedAnimationParameter, _characterMotion.CurrentSpeed);
		}

		private void OnDestroy()
		{
			_characterMotion.OnJump -= PlayJump;
			_characterMotion.OnFall -= PlayFall;
			_characterMotion.OnLand -= PlayLand;
		}

		private void PlayJump() =>
			Animator.SetTrigger(JumpAnimationParameter);

		private void PlayFall() =>
			Animator.SetTrigger(FallAnimationParameter);

		private void PlayLand() =>
			Animator.SetTrigger(LandAnimationParameter);
	}
}
