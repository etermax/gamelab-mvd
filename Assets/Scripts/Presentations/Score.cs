using UnityEngine;

namespace Presentations
{
    public class Score : MonoBehaviour
    {
        public int score; // The player's score.
        private PlayerControl playerControl; // Reference to the player control script.
        private int previousScore; // The score in the previous frame.
        public int highestScore;

        void Awake()
        {
            // Setting up the reference.
            playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        }

        void Update()
        {
            // Set the score text.
            GetComponent<GUIText>().text = "Score: " + score +
                                           (highestScore > 0 ? "(Max: " + highestScore + ")" : "");
            
            // If the score has changed...
            if (previousScore != score)
                // ... play a taunt.
                playerControl.StartCoroutine(playerControl.Taunt());

            // Set the previous score to this frame's score.
            previousScore = score;
        }
    }
}