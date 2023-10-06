using UnityEngine;

namespace Game
{
    public class EnemiesInitializer : MonoBehaviour
    {
		[SerializeField] private Transform _hero;

		private void Start()
		{
			EnemyController[] enemies = GetComponentsInChildren<EnemyController>();

			foreach (var enemy in enemies)
				enemy.Initialize(_hero);
        }
	}
}