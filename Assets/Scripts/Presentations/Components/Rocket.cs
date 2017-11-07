using Presentations.Game;
using UnityEngine;

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


	private void DoExplode()
	{
		var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		Instantiate(explosion, transform.position, randomRotation);
	}

	private void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.CompareTag("Enemy"))
			gameCotroller.OnRocketImpactsEnemy(this, col.gameObject.GetComponent<Enemy>());
		
		else if(col.CompareTag("BombPickup"))
			gameCotroller.OnRocketImpactsBomb(this, col.gameObject.GetComponent<Bomb>());
		
		// Otherwise if the player manages to shoot himself...
		else if(!col.gameObject.CompareTag("Player"))
		{
			gameCotroller.OnRocketImpactsWithSomethingElse(this);
		}
	}

	public void Explode()
	{
		DoExplode();
		Destroy (gameObject);
	}
}
