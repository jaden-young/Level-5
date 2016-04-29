using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DragonAI : MonoBehaviour 
{
	// public variables are set in editor
	public float attackCoolDown;
	public int dragonDamage;
	public float rotationSpeed;
	public float attackRadius;
	public Transform player;
	public NavMeshAgent nav;
	public Animator anim;
	public const int STARTING_HEALTH = 100;
	public Slider healthSlider;

	private int currentHealth;
	private bool isDead;
	private int halfHealth = STARTING_HEALTH / 2;
	private const int HEADSWIPE_RANGE = 13;
	private const int FLUTTER_KICK_RANGE = 8;
	private float attackTimer = 0f;
	private bool hasFlown = false;
	private bool isFlying = false;
	private bool isAttacking; // This will be set by animation events

	void Awake ()
	{
		anim = GetComponent <Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav.updateRotation = true;
		currentHealth = STARTING_HEALTH;
	}

	void Start ()
	{
		Roar ();
	}

	void Update () 
	{

		/*
		 * We only want to check for a new state to enter when the dragon isn't currently
		 * attacking. This has an affect of waiting for the attack animation to complete
		 * before doing anything else.
		 */
		if (isAttacking || isDead || attackTimer < attackCoolDown) {
			attackTimer += Time.deltaTime;
			return;
		}

		if (isFlying) {
			Fly ();
			return;
		}

		// Let's check for a new state to enter

		// Take off and fly around for a bit when we reach half health
		if (currentHealth <= halfHealth && !hasFlown) {
			isFlying = true; // makes us start flying on next frame
			hasFlown = true;
			anim.Play ("TakeOff");
			return;
		}

		// variables we'll need to determine state
		Vector3 heading = player.position - transform.position;       // points from dragon to player
		heading.y = 0;                                                // don't care about height difference	
		float distance = heading.magnitude;                           // distance between dragon and player
		float angle = Vector3.Angle(transform.forward, heading);      // angle between dragon and player

		// we're close enogh for longest attack
		if (distance < HEADSWIPE_RANGE) {
			// target is in front of us
			if (angle < attackRadius) {
				// check shorter attack first
				if (distance < FLUTTER_KICK_RANGE) {
					FlutterKick ();
				} else {
					HeadSwipe ();
				}
				// we're close, but target is not in front
			} else {
				RotateTowards (heading);
			}
			// we're too far away to attack, move towards target
		} else {
			Chase ();
		}
		//attackTimer += Time.deltaTime;
	}

	public void TakeDamage (int amount)
	{
		currentHealth -= amount;

		healthSlider.value = currentHealth;

		if (currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	void Death () {
		isDead = true;
	}

	// For use with animation events. See similar method in PlayerAttack for more info.
	void SetIsAttacking (int a)
	{
		if (a == 1) {
			isAttacking = true;
		} else {
			isAttacking = false;
		}
	}

	// Called when the colliders marked as triggers collide with an object.
	// There is on one each front leg for the flutter kick, as well as one on 
	// the dragons head. These give the attacks a wider hit box.
	void OnTriggerEnter (Collider other) 
	{
		if (isAttacking && other.CompareTag("Player")) {
			other.SendMessageUpwards ("TakeDamage", dragonDamage, SendMessageOptions.DontRequireReceiver);

			// Set to false so we only deal damage once per attack
			isAttacking = false;
		}
	}

	void RotateTowards(Vector3 heading) {
		anim.Play ("Walk");
		Quaternion lookRotation = Quaternion.LookRotation (heading.normalized);
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, rotationSpeed);
	}

	void Roar () 
	{
		anim.Play ("Roar");
	}
	void FlutterKick ()
	{
		attackTimer = 0f;
		nav.Stop ();
		anim.Play ("FlutterKick");
	}

	void HeadSwipe ()
	{
		attackTimer = 0f;
		nav.Stop ();
		anim.Play ("HeadSwipe");
	}

	void Fly ()
	{

	}

	void Chase ()
	{
		nav.destination = player.position;
		nav.Resume ();
		anim.Play ("Walk");
	}

}

