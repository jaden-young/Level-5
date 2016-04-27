using UnityEngine;
using System.Collections;

public class DragonMovement : MonoBehaviour 
{

	Animator anim;

	private PlayerHealth playerHealth;
	private Transform player;
	private NavMeshAgent nav;

	void Awake ()
	{
		anim = GetComponent <Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
	}

	// Update is called once per frame
	void Update () 
	{
		nav.destination = player.position;
		anim.Play ("Walk");
	}
}

