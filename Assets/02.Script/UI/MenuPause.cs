using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject PausePanel;
    void Start()
    {
        this.Continue();
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Creditos()
    {
        CreditosActions.OpenScene();
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        HomeActions.OpenScene();
        Time.timeScale = 0;
    }
}
