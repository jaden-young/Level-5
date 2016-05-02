using UnityEngine;
using System.Collections;

public class BottleOfRanchController : MonoBehaviour {

	private Animator anim;
	public bool gameOver;      // Will be accessed by SceneManager

	void Start () 
	{
		anim = GetComponent<Animator> ();
		gameOver = false;
	}

	// Will be called by ChestController
	public void RaiseBottle () 
	{
		anim.Play ("RanchRise");
	}

	// Will be called by an animation event at the end of the ranch bottle rising
	void GameOver () 
	{
		gameOver = true;
	}

}
