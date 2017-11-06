using UnityEngine;
using System.Collections;
using Presentation.Player.Presenter;
using Presentation.Player.View;

public class PlayerControl : MonoBehaviour, PlayerView
{
    [HideInInspector] public bool facingRight = true; // For determining which way the player is currently facing.
    [HideInInspector] public bool jump;

    float moveForce = 365f; // Amount of force added to move the player left and right.
    float maxSpeed = 5f; // The fastest the player can travel in the x axis.
    public AudioClip[] jumpClips; // Array of clips for when the player jumps.
    float jumpForce = 1000f; // Amount of force added when the player jumps.
    public AudioClip[] taunts; // Array of clips for when the player taunts.
    float tauntProbability = 50f; // Chance of a taunt happening.
    float tauntDelay = 1f; // Delay for when the taunt should happen.

    int tauntIndex; // The index of the taunts array indicating the most recent taunt.
    Transform groundCheck; // A position marking where to check if the player is grounded.
    PlayerControlPresenter presenter;

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        presenter = new PlayerControlPresenter(this);
        Initialize();
    }

    private void Initialize()
    {
        presenter.Initialize(GetComponent<Animator>());
        if (Input.GetButtonDown("Jump"))
            presenter.OnJumpButtonPressed();
    }


    void Update()
    {
        presenter.OnUpdate();
    }

    void FixedUpdate()
    {
        var hInput = Input.GetAxis("Horizontal");
        presenter.OnFixedUpdate(hInput);
        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (hInput * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            // ... add a force to the player.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * hInput * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (ShouldFlip(hInput))
            Flip();

        if (jump)
        {
            doJump();
        }
    }

    private bool ShouldFlip(float hAxis)
    {
        return hAxis > 0 && !facingRight || hAxis < 0 && facingRight;
    }

    private void doJump()
    {
        presenter.OnJump();
        var i = Random.Range(0, jumpClips.Length);
        AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        jump = false;
    }


    void Flip()
    {
        SwitchFacing();

        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void SwitchFacing()
    {
        facingRight = !facingRight;
    }

    public IEnumerator Taunt()
    {
        // Check the random chance of taunting.
        var tauntChance = Random.Range(0f, 100f);
        if (!(tauntChance > tauntProbability)) yield break;
        // Wait for tauntDelay number of seconds.
        yield return new WaitForSeconds(tauntDelay);

        // If there is no clip currently playing.
        if (!GetComponent<AudioSource>().isPlaying)
        {
            // Choose a random, but different taunt.
            tauntIndex = TauntRandom();

            // Play the new taunt.
            GetComponent<AudioSource>().clip = taunts[tauntIndex];
            GetComponent<AudioSource>().Play();
        }
    }


    int TauntRandom()
    {
        var random = Random.Range(0, taunts.Length);
        return random == tauntIndex ? TauntRandom() : random;
    }

    public bool isPlayerGrounded()
    {
        return Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void Jump()
    {
        jump = true;
    }
}