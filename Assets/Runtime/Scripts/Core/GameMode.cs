using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private EndlessPipeGenerator pipeGenerator;

    [SerializeField] private GameSaver gameSaver;

    [SerializeField] private ScreenController screenController;

    [SerializeField] private AudioService audioService;

    [Header("Data")]
    [SerializeField] private PlayerMovementParameters gameRunningParameters;
    [SerializeField] private PlayerMovementParameters waitingGameStartParameters;

    [SerializeField] private PlayerMovementParameters gameOverParameters;

    [Header("Audio")]
    [SerializeField] private AudioClip fallAudio;
    [SerializeField] private float fallAudioDelay = 0.3f;

    public int Score { get; private set; }
    public int BestScore => gameSaver.CurrentSave.HighestScore < Score ? Score : gameSaver.CurrentSave.HighestScore;

    private void Awake()
    {
        gameSaver.LoadGame();
        playerController.MovementParameters = waitingGameStartParameters;
        AudioUtility.AudioService = audioService;

        screenController.ShowWaitGameStartScreen();
    }

    public void StartGame()
    {
        playerController.MovementParameters = gameRunningParameters;
        playerController.Flap();
        pipeGenerator.StartPipeSpawn();
        screenController.ShowInGameHud();
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
        gameSaver.SaveGame(new SaveGameData
        {
            HighestScore = BestScore
        });
        StartCoroutine(GameOverCor());
    }

    private IEnumerator GameOverCor()
    {
        screenController.ShowGameOverScreen();
        yield return new WaitForSeconds(fallAudioDelay);
        if (!playerController.IsOnGroud)
        {
            AudioUtility.PlayAudioCue(fallAudio);
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void IncrementScore()
    {
        Score++;
    }
}
