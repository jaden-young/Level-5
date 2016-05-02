using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelFiveManager : MonoBehaviour {

	[SerializeField] BottleOfRanchController ranchController;
	[SerializeField] PlayerHealth playerHealth;

	/*
	 * It would be more efficient to simply have the game over in the 
	 * ranch script, but using this to make it obvious what causes
	 * the game to end.
	 */
	void Update () 
	{
		if (ranchController.gameOver || playerHealth.isDead) {
			SceneManager.LoadScene (0);
		}
	}
}