using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ModalFeedbackAction : MonoBehaviour
{
    private static GameObject modalFeedbackPreFab;
    private static Action onResposta;
    private static bool sucess;

    public static void setModalFeedback(GameObject preFab)
    {
        modalFeedbackPreFab = preFab;
    }
    public static void OpenModal(string title, string complemento, Action resposta)
    {
        modalFeedbackPreFab.SetActive(true);
        setTitulo(title);
        setComplemento(complemento);
        onResposta = resposta;
    }

    public static void OpenSuccessModal(string title, string complemento, Action resposta)
    {
        sucess = true;
        OpenModal(title, complemento, resposta);
    }

    public static void OpenErrorModal(string title, string complemento, Action resposta)
    {
        sucess = false;
        OpenModal(title, complemento, resposta);
    }

    public static void CloseModal()
    {
        modalFeedbackPreFab.SetActive(false);
    }

    private static void setTitulo(string titulo)
    {
        var goTitulo = modalFeedbackPreFab.GetComponentsInChildren<Text>().FirstOrDefault(g => g.name == "Titulo");

        setColor(goTitulo);
        goTitulo.text = titulo;

    }

    private static void setComplemento(string complemento)
    {
        var goComplemento = modalFeedbackPreFab.GetComponentsInChildren<Text>().FirstOrDefault(g => g.name == "Complemento");
        goComplemento.text = complemento;
    }

    private static void setColor(Text goTitulo)
    {
        Color color;

        if (sucess)
        {
            UnityEngine.ColorUtility.TryParseHtmlString("#6CAD61", out color);
            goTitulo.color = color;
        }
        else
        {
            UnityEngine.ColorUtility.TryParseHtmlString("#e74c3c", out color);
            goTitulo.color = color;
        }
    }

    public static void OnClickOk()
    {
        onResposta.Invoke();

        CloseModal();
    }
}
