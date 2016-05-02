using UnityEngine;
using UnityEngine.UI;
using System.Collections;
	
public class PlayerAttack : MonoBehaviour 
{
	// Serialized fields set in editor
	[SerializeField] private float attackCoolDown; // Minimum time between attacks
	[SerializeField] private int swordDamage;      // Damage for each attack to cause
	[SerializeField] private AudioClip swordWoosh;
	[SerializeField] private AudioClip swordHit;

	private bool isAttacking; // This will be set by Animation Events in Sword
	private Animator anim;
	private AudioSource audio;

	void Awake ()
	{
		anim = GetComponent <Animator> ();
		audio = GetComponent <AudioSource> ();
		isAttacking = false;
	}
		
	void Start ()
	{
		//Debug.Log(GetComponentInChildren<BoxCollider> ().gameObject);
		// FirstPersonCharacter is the direct child of the Player object to which this script is atattched.
		// For some reason, god knows why, I can't get the capsule collider from it.
		//Debug.Log(GetComponentInChildren<CapsuleCollider> (true).gameObject); // true to check for inactive objects
		// EVEN THOUGH FIRST PERSON CHARACTER IS ACTIVE

		// Idea is to not have the pysics engine calculate collision data between our character's
		// hit box (capsule) and the sword (box). Moving the sword farther away just looks silly,
		// and making the characters box smaller makes it too hard to hit.

		// want to run
		// Physics.IgnoreCollision(GetComponentInChildren<BoxCollider> (), GetComponentInChildren<CapsuleCollider>());

		// Ah well, unity just gets more calculations to deal with.
	}

	// When the box collider of the Sword collides with something
	void OnCollisionEnter (Collision col)
	{
		// Collision has a whole bunch of info we don't really care about, only want to know
		// what we collided with.
		Collider other = col.collider;

		if (other.CompareTag ("Dragon") && isAttacking)
		{
			audio.clip = swordHit;
			audio.Play ();

			// Tells the dragon to look for a script attached to it that has the method 
			// TakeDamage and call it with an argument of our swordDamage (int 5).
			other.SendMessageUpwards ("TakeDamage", swordDamage, SendMessageOptions.DontRequireReceiver);

			// Only want to cause damage once per swing.
			// If we don't set this here, dragon takes damage for every collider the sword hits.
			isAttacking = false;
		}
	}
				
	void Update () 
	{
		// Timing of attacks is controlled by the animation events. Only
		// able to attack again once attack animation is finished.
		if (Input.GetMouseButtonDown (0) && !isAttacking) 
		{
			Attack ();
		}			
	}
		
	/*
	 * This may be ugly, but at the same time it's beautiful.
	 * Unity animations can have custom Events triggered at certain frames
	 * that call methods in the script that the animation controller is attached
	 * to. Unfortunately, they can only pass ints, floats, strings, and objects, not booleans.
	 * Since this script and the SwordController animation controller are both
	 * components of the Sword game object, the SwordSwing animation events at the start
	 * and end look for a script in Sword, find this script, and call the method
	 * SetIsAttacking. The event at the beginning passes 1, and the event at the 
	 * end passes 0.
	 */
	void SetIsAttacking (int a)
	{
		if (a == 1) 
		{
			isAttacking = true;
		} 
		else 
		{
			isAttacking = false;
		}
	}
		
	void Attack () 
	{
		anim.Play ("SwordSwing");
		audio.clip = swordWoosh;
		audio.Play ();
	}
		
}
