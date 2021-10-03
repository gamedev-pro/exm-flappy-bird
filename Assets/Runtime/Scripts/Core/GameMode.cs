using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private EndlessPipeGenerator pipeGenerator;

    [SerializeField] private ScreenController screenController;

    [Header("Data")]
    [SerializeField] private PlayerMovementParameters gameRunningParameters;
    [SerializeField] private PlayerMovementParameters gameOverParameters;

    public int Score { get; private set; }

    private void Awake()
    {
        StartGame();
    }

    public void StartGame()
    {
        playerController.MovementParameters = gameRunningParameters;
        playerController.Flap();
        pipeGenerator.StartPipeSpawn();
        screenController.ShowInGameHud();
    }

    public void GameOver()
    {
        playerController.MovementParameters = gameOverParameters;
        StartCoroutine(GameOverCor());
    }

    private IEnumerator GameOverCor()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncrementScore()
    {
        Score++;
    }
}
