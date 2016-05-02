using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour {

	[SerializeField] private DragonAI dragonAI;

	private Animator anim;
	private AudioSource audio;
	private bool done;        // So we don't open the gate over and over

	void Start () 
	{
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		done = false;
	}
	
	void Update () 
	{
		if (dragonAI.isDead && !done) {
			anim.Play ("OpenGate");
			audio.Play ();
			done = true;
		}
	}
}
