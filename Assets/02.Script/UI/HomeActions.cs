using Assets._02.Script.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeActions : MonoBehaviour
{
    private const string prefixScene = "01.Scenes/";
    private const string HomeScene = prefixScene + "01.Home";
    private const string InitalScene = prefixScene + "02.ExternoIF";

    void Update()
    {
        if (Controls.confirmAction())
            OpenScene();

        if (Controls.escapeAction())
            ExitGame();
    }

    public static void OpenScene()
    {
        SceneManager.LoadScene(HomeScene);
    }

    public void OpenInitialScene()
    {
        SceneManager.LoadScene(InitalScene);
    }

    public void Creditos()
    {
        CreditosActions.OpenScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
