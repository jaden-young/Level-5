using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class DragonHealth : MonoBehaviour 
{

	public const int STARTING_HEALTH = 100;
	public Slider healthSlider; // Set in editor
	public int currentHealth;

	bool isDead;

	void Awake()
	{
		currentHealth = STARTING_HEALTH;
	}

	public void TakeDamage (int amount)
	{
		currentHealth -= amount;

		healthSlider.value = currentHealth;

		if (currentHealth <= 0 && !isDead)
		{
			Death();
		}
	}

	void Death ()
	{
		isDead = true;
		// end game stufferoony goes here
	}
}