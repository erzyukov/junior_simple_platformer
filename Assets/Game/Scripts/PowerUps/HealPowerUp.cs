using UnityEngine;

namespace Game
{
    public class HealPowerUp : Collectable
	{
		[SerializeField] private int _healAmount;

		override protected void OnTriggerEnter2D(Collider2D collision)
		{
			base.OnTriggerEnter2D(collision);

			Viability viabilityComponent = collision.GetComponent<Viability>();

			if (viabilityComponent == null)
				return;
				
			viabilityComponent.Heal(_healAmount);
			gameObject.GetComponent<Collider2D>().enabled = false;
		}
	}
}