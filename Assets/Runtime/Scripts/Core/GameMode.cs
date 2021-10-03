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

    [Header("Data")]
    [SerializeField] private PlayerMovementParameters gameRunningParameters;
    [SerializeField] private PlayerMovementParameters waitingGameStartParameters;

    [SerializeField] private PlayerMovementParameters gameOverParameters;

    public int Score { get; private set; }
    public int BestScore => gameSaver.CurrentSave.HighestScore < Score ? Score : gameSaver.CurrentSave.HighestScore;

    private void Awake()
    {
        gameSaver.LoadGame();
        playerController.MovementParameters = waitingGameStartParameters;
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
        screenController.ShowGameOverScreen();
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
