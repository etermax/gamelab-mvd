using UnityEngine;
using System.Collections;
using Presentation.Game;

public class BombPickup : MonoBehaviour
{
	public AudioClip pickupClip;		// Sound for when the bomb crate is picked up.


	private Animator anim;				// Reference to the animator component.
	private bool landed = false;		// Whether or not the crate has landed yet.
	private GameController gameCotroller;

	void Awake()
	{
		// Setting up the reference.
		anim = transform.root.GetComponent<Animator>();
		gameCotroller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.CompareTag("Player"))
		{
			// ... play the pickup sound effect.
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);

			gameCotroller.OnBombPickedUp();

			// Destroy the crate.
			Destroy(transform.root.gameObject);
		}
		// Otherwise if the crate lands on the ground...
		else if(other.CompareTag("ground") && !landed)
		{
			// ... set the animator trigger parameter Land.
			anim.SetTrigger("Land");
			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;		
		}
	}
}
