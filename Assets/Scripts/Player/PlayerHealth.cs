using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{

	public const int STARTING_HEALTH = 100;
	public int currentHealth;
	public Slider healthSlider;
	//public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

	public bool isBlocking { get; set; }

	bool takingDamage; // For flashing screen red when player is hit
	bool isDead;

	void Awake()
	{
		currentHealth = STARTING_HEALTH;
	}

	void Update ()
	{
		if (takingDamage)
		{
			//damageImage.color = flashColor;
		} 
		else
		{
			//damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed = Time.deltaTime);
		}

		//  set damage to false at the end of every frame
		takingDamage = false;
	}

	public void TakeDamage (int amount)
	{
		takingDamage = true;

		if (isBlocking) 
		{
			amount /= 2;
		}

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