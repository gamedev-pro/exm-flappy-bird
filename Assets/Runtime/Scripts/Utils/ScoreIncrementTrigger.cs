using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(AudioCue))]
public class ScoreIncrementTrigger : MonoBehaviour
{
    public GameMode GameMode { private get; set; }
    private AudioCue audioCue;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        audioCue = GetComponent<AudioCue>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameMode != null && other.GetComponent<PlayerController>() != null)
        {
            GameMode.IncrementScore();
            audioCue.Play();
        }
    }
}
