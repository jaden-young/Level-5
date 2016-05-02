using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
	// Will be set in PlayerDefend script
	public bool isBlocking { get; set; }
	// Will be accessed by LevelFiveManager
	public bool isDead;

	// Serialized fields set in editor
	[SerializeField] private int startingHealth;
	[SerializeField] private Slider healthSlider;
	[SerializeField] private Image damageImage;
	[SerializeField] private float flashSpeed;
	[SerializeField] private Color flashColor;

	[Header("Audio Clips")]
	[Space(5)]
	[SerializeField] private AudioClip playerDamage;
	[SerializeField] private AudioClip playerDeath;
	[SerializeField] private AudioClip shieldHit;

	private AudioSource audio;
	private int currentHealth;
	private bool takingDamage; // For flashing screen red when player is hit


	void Awake()
	{
		audio = GetComponent <AudioSource> ();
		currentHealth = startingHealth;
	}

	void Update ()
	{
		if (takingDamage)
		{
			// Show red image
			damageImage.color = flashColor;
		} 
		else
		{
			// Fade to clear
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		//  set damage to false at the end of every frame
		takingDamage = false;
	}

	public void TakeDamage (int amount)
	{
		// Causes damage image to flash in Update
		takingDamage = true;

		if (isBlocking) {
			audio.clip = shieldHit;
			audio.Play ();
			amount /= 2;
		} else {
			audio.clip = playerDamage;
			audio.Play ();
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
		audio.clip = playerDeath;
		audio.Play ();
		// end game stufferoony goes here
	}
}