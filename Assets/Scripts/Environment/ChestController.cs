using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {

	[SerializeField] BottleOfRanchController ranch;

	// So we don't open multiple times if the player backs off and reenters
	private bool isOpen;

	private Animation anim;
	private AudioSource audio;


	void Start () 
	{
		anim = GetComponent<Animation> ();
		audio = GetComponent<AudioSource> ();
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player") && !isOpen) {
			anim.Play ("open");
			audio.Play ();
			ranch.RaiseBottle();
			isOpen = true;
		}
	}
}
