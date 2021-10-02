using System;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] screens;

    private GameObject currentScreen;

    private void Awake()
    {
        foreach (GameObject screen in screens)
        {
            screen.gameObject.SetActive(false);
        }
    }

    public void ShowInGameHud()
    {
        ShowScreen(FindScreenWithComponent(typeof(InGameHudScreen)));
    }

    public void ShowPauseScreen()
    {
        ShowScreen(FindScreenWithComponent(typeof(PauseScreen)));
    }

    public void ShowWaitGameStartScreen()
    {
        ShowScreen(FindScreenWithComponent(typeof(WaitGameStartScreen)));
    }

    public void ShowGameOverScreen()
    {
        ShowScreen(FindScreenWithComponent(typeof(GameOverScreen)));
    }

    private void ShowScreen(GameObject screen)
    {
        CloseCurrent();
        screen.SetActive(true);
        currentScreen = screen;
    }

    private void CloseCurrent()
    {
        if (currentScreen != null)
        {
            currentScreen.gameObject.SetActive(false);
        }
    }

    //TODO: Use generics
    private GameObject FindScreenWithComponent(Type type)
    {
        foreach (GameObject screen in screens)
        {
            if (screen.GetComponent(type) != null)
            {
                return screen;
            }
        }
        return null;
    }
}
