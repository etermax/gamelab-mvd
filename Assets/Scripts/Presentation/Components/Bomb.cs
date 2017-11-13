using UnityEngine;
using System.Collections;

public interface IBomb
{
	void Explode();
}

public class Bomb : MonoBehaviour, IBomb
{
	public float bombRadius = 10f;			// Radius within which enemies are killed.
	public float bombForce = 100f;			// Force that enemies are thrown from the blast.
	public AudioClip boom;					// Audioclip of explosion.
	public AudioClip fuse;					// Audioclip of fuse.
	public float fuseTime = 1.5f;
	public GameObject explosion;			// Prefab of explosion effect.


	private LayBombs layBombs;				// Reference to the player's LayBombs script.
	private PickupSpawner pickupSpawner;	// Reference to the PickupSpawner script.
	private ParticleSystem explosionFX;		// Reference to the particle system of the explosion effect.


	void Awake ()
	{
		// Setting up references.
		explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
		pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
		if(GameObject.FindGameObjectWithTag("Player"))
			layBombs = GameObject.FindGameObjectWithTag("Player").GetComponent<LayBombs>();
	}

	void Start ()
	{
		// If the bomb has no parent, it has been laid by the player and should detonate.
		if(transform.root == transform)
			StartCoroutine(BombDetonation());
	}


	IEnumerator BombDetonation()
	{
		// Play the fuse audioclip.
		AudioSource.PlayClipAtPoint(fuse, transform.position);

		// Wait for 2 seconds.
		yield return new WaitForSeconds(fuseTime);

		// Explode the bomb.
		Explode();
	}


	public void Explode()
	{
		LetPlayerLayBombs();
		pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());
		KillEnenmiesCLoseToBomb();
		DrawExplosion();
		GetValuePlayExplosionFX();
		Destroy (gameObject);
	}

	private void LetPlayerLayBombs()
	{
		layBombs.bombLaid = false;
	}

	private void GetValuePlayExplosionFX()
	{
		AudioSource.PlayClipAtPoint(boom, transform.position);
	}

	private void DrawExplosion()
	{
		explosionFX.transform.position = transform.position;
		explosionFX.Play();
		Instantiate(explosion, transform.position, Quaternion.identity);
	}

	private void KillEnenmiesCLoseToBomb()
	{
		// Find all the colliders on the Enemies layer within the bombRadius.
		var enemies = FindEnemiesOnTheRadio();

		// For each collider...
		foreach (var en in enemies)
		{
			// Check if it has a rigidbody (since there is only one per enemy, on the parent).
			var rigidBody = en.GetComponent<Rigidbody2D>();
			if (rigidBody != null && rigidBody.CompareTag("Enemy"))
			{
				// Find the Enemy script and set the enemy's health to zero.
				rigidBody.gameObject.GetComponent<Enemy>().Life = 0;

				// Find a vector from the bomb to the enemy.
				var deltaPos = rigidBody.transform.position - transform.position;

				// Apply a force in this direction with a magnitude of bombForce.
				var force = deltaPos.normalized * bombForce;
				rigidBody.AddForce(force);
			}
		}
	}

	private Collider2D[] FindEnemiesOnTheRadio()
	{
		return Physics2D.OverlapCircleAll(transform.position, bombRadius, 1 << LayerMask.NameToLayer("Enemies"));
	}
}


