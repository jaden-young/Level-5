using UnityEngine;
using System.Collections;

/*
 * Simply controls the animation of the shield.
 * Raises the shield if right mouse button is down,
 * lowers the shield if not.
 */
public class PlayerDefend : MonoBehaviour {

	Animator anim;
	PlayerHealth playerHealth;

	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerHealth = GetComponentInParent<PlayerHealth> ();
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown (1)) 
		{
			anim.Play ("RaiseShield");
		}

		if (Input.GetMouseButtonUp (1))
		{
			anim.Play ("LowerShield");
		}
	}

	/*
	 * This is called by Animation Events in the Shield
	 * See SetIsAttacking in PlayerAttack script for more explaination
	 */
	void SetIsBlocking (int a)
	{
		if (a == 1) 
		{
			playerHealth.isBlocking = true;
		} 
		else 
		{
			playerHealth.isBlocking = false;
		}
	}
}
