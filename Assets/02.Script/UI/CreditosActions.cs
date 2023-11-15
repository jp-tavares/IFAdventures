using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditosActions : MonoBehaviour
{
    private const string prefixScene = "01.Scenes/";
    private const string CreditosScene = prefixScene + "99.Creditos";
    private static string previousScene;

    public static void OpenScene()
    {
        var currentScene = prefixScene + SceneManager.GetActiveScene().name;
        previousScene = currentScene;
        SceneManager.LoadScene(CreditosScene);
    }

    public void closeCreditos()
    {
        SceneManager.LoadScene(CreditosActions.previousScene);
    }
}
