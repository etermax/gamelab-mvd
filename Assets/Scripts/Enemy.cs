using UnityEngine;
using System.Linq;
using DefaultNamespace;

public interface IEnemy
{
	void Hurt();
	void Death();
	void Flip();
	int GetHealth();
	void SetDamagedState();
	bool IsStrongEnemy();
	bool IsDeath();
}

public class Enemy : MonoBehaviour, IEnemy
{
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int HP = 2;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying


	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.

	private GameController gameCotroller;

	
	void Awake()
	{
		ren = transform.Find("body").GetComponent<SpriteRenderer>();
		frontCheck = transform.Find("frontCheck").transform;
		gameCotroller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	void FixedUpdate ()
	{
		CheckObstacleHits();
		UpdateSpeedMovement();	
	}

	private void UpdateSpeedMovement()
	{
		GetComponent<Rigidbody2D>().velocity =
			new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}

	private void CheckObstacleHits()
	{
		var frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);

		if (frontHits.Any(c => c.CompareTag("Obstacle")))
			gameCotroller.OnEnemyHitsWithObstacle(this);
	}

	public void Hurt()
	{
		HP--;
	}

	public void SetDamagedState()
	{
		ren.sprite = damagedEnemy;
	}

	public bool IsStrongEnemy()
	{
		return damagedEnemy != null;
	}

	public bool IsDeath()
	{
		return dead;
	}

	public void Death()
	{
		DisableSpriteRenders();
		SetDeathSprite();
		dead = true;
		MakeMeTriggerWithEverithing();
		PlayADeathSound();
		DrawPointsScore();
	}

	private void DrawPointsScore()
	{
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;
		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
	}

	private void PlayADeathSound()
	{
		var i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);
	}

	private void MakeMeTriggerWithEverithing()
	{
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));
		var cols = GetComponents<Collider2D>();
		foreach (var c in cols) c.isTrigger = true;
	}

	private void SetDeathSprite()
	{
		ren.enabled = true;
		ren.sprite = deadEnemy;
	}

	private void DisableSpriteRenders()
	{
		var otherRenderers = GetComponentsInChildren<SpriteRenderer>();
		foreach (var s in otherRenderers) s.enabled = false;
	}


	public void Flip()
	{
		var enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}

	public int GetHealth()
	{
		return HP;
	}
}
