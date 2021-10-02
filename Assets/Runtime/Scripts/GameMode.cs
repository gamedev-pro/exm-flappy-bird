using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private EndlessPipeGenerator pipeGenerator;

    [SerializeField] private ScreenController screenController;

    [SerializeField] private AudioService audioService;

    [Header("Data")]
    [SerializeField] private PlayerMovementParameters gameRunningParameters;
    [SerializeField] private PlayerMovementParameters waitingGameStartParameters;

    [SerializeField] private PlayerMovementParameters gameOverParameters;

    [Header("Audio")]
    [SerializeField] private AudioClip fallAudio;
    [SerializeField] private float fallAudioDelay = 0.3f;

    private bool isGameRunning;

    public int Score { get; private set; }

    private void Awake()
    {
        playerController.MovementParameters = waitingGameStartParameters;
        screenController.ShowWaitGameStartScreen();
        AudioUtility.AudioService = audioService;
    }

    public void StartGame()
    {
        playerController.MovementParameters = gameRunningParameters;
        playerController.Flap();
        pipeGenerator.StartPipeSpawn();
        screenController.ShowInGameHud();
        isGameRunning = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        screenController.ShowPauseScreen();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        screenController.ShowInGameHud();
    }

    public void GameOver()
    {
        playerController.MovementParameters = gameOverParameters;
        StartCoroutine(GameOverCor());
    }

    private IEnumerator GameOverCor()
    {
        yield return new WaitForSeconds(fallAudioDelay);
        if (!playerController.IsOnGroud)
        {
            AudioUtility.PlayAudioCue(fallAudio);
        }
    }

    private IEnumerator ReloadGameCor()
    {
        playerController.enabled = false;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnHitGround()
    {
        StartCoroutine(ReloadGameCor());
    }

    public void IncrementScore()
    {
        Score++;
    }
}
