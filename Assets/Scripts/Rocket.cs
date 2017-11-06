using UnityEngine;
using DefaultNamespace;

public interface IRocket
{
	void Explode();
}

public class Rocket : MonoBehaviour, IRocket
{
	public GameObject explosion;		// Prefab of explosion effect.
	private GameController gameCotroller;

	private void Awake()
	{
		gameCotroller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}


	void DoExplode()
	{
		var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.tag == "Enemy")
		{
			gameCotroller.OnRocketImpactsEnemy(this, col.gameObject.GetComponent<Enemy>());
		}
		// Otherwise if it hits a bomb crate...
		else if(col.tag == "BombPickup")
		{
			gameCotroller.OnRocketImpactsHealtPack(this, col.gameObject.GetComponent<Bomb>());
			
			
			// ... find the Bomb script and call the Explode function.
			col.gameObject.GetComponent<Bomb>().Explode();

			// Destroy the bomb crate.
			Destroy (col.transform.root.gameObject);

			// Destroy the rocket.
			Destroy (gameObject);
		}
		// Otherwise if the player manages to shoot himself...
		else if(col.gameObject.tag != "Player")
		{
			// Instantiate the explosion and destroy the rocket.
			DoExplode();
			Destroy (gameObject);
		}
	}

	public void Explode()
	{
		DoExplode();
		Destroy (gameObject);
	}
}
