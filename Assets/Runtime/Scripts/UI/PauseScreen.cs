using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    public void OnResumeClicked()
    {
        gameMode.ResumeGame();
    }
}
